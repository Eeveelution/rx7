using System;
using System.Collections.Generic;
using System.IO;
using _13B_REW.Bancho.Database;
using _13B_REW.Bancho.Managers;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Objects;
using _13B_REW.Bancho.Packets;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects.Serializables;
using EeveeTools.Database;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;
using MySqlConnector;

namespace _13B_REW.Bancho {
    public class ClientOsu : TcpClientHandler {
        public DatabaseUser DatabaseUser = null;

        public UserStats         UserStats       = null;
        public UserPresence      UserPresence    = null;
        public ClientInformation UserInformation = null;

        public readonly List<Channel> JoinedChannels = new();

        public readonly  List<ClientOsu> Spectators       = new();
        private readonly object          _spectatorLock   = new();
        public           ClientOsu       SpectatingClient = null;
        protected override void HandleData(byte[] data) {
            if (this.UserStats == null || this.UserPresence == null || this.UserInformation == null || DatabaseUser == null) {
                StreamReader loginReader = new(new MemoryStream(data));

                string username = loginReader.ReadLine();
                string password = loginReader.ReadLine();
                string clientData = loginReader.ReadLine();

                this.HandleLogin(username, password, clientData);
            } else {
                this.HandlePacketData(new MemoryStream(data));
            }
        }

        private void HandleLogin(string username, string password, string clientData) {
            try {
                string[] splitData = clientData?.Split("|");

                if (splitData == null) {
                    this.LoginResult(LoginResult.OutdatedClient);
                    return;
                }

                string version = splitData[0];
                string timezone = splitData[1];
                string showCityLocation = splitData[2];

                string multiaccountPreventionData = splitData[3];

                string[] splitNetworkData = multiaccountPreventionData.Split(":");

                this.UserInformation = new ClientInformation() {
                    ClientVersion         = version,
                    Timezone              = int.Parse(timezone),
                    CityLocationDisplayed = showCityLocation == "1",

                    //Peppy legit stalking us through physical network adresses
                    HashedCommandLineArgs         = splitNetworkData[0],
                    PhysicalNetworkAdresses       = splitNetworkData[1].Split("."),
                    PhysicalNetworkAdressesHashed = splitNetworkData[2]
                };

                const string userFetchSql = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY StandardRankedScore DESC) AS 'StandardRank', ROW_NUMBER() OVER (ORDER BY TaikoRankedScore DESC) AS 'TaikoRank', ROW_NUMBER() OVER (ORDER BY CatchRankedScore DESC) AS 'CatchRank' FROM users) t WHERE Username=@username";

                MySqlParameter[] userFetchParams = new[] {
                    new MySqlParameter("@username", username)
                };

                IReadOnlyDictionary<string, object>[] userFetchResults = DatabaseHandler.Query(GlobalVariables.DatabaseContext, userFetchSql, userFetchParams);

                if (userFetchResults.Length == 0) {
                    this.LoginResult(LoginResult.WrongLogin);
                    return;
                }

                this.DatabaseUser = new DatabaseUser();
                this.DatabaseUser.MapObject(userFetchResults[0]);

                this.UserStats = new UserStats() {
                    UserId      =         this.DatabaseUser.UserId,
                    Rank        =         this.DatabaseUser.StandardRank,
                    Accuracy    = (float) this.DatabaseUser.StandardAccuracy / 100f,
                    Playcount   =         this.DatabaseUser.StandardPlaycount,
                    RankedScore =         this.DatabaseUser.StandardRankedScore,
                    StatusUpdate = new StatusUpdate() {
                        Action          = "User Just Logged in!",
                        BeatmapChecksum = "none",
                        BeatmapId       = -1,
                        EnabledMods     = 0,
                        PlayMode        = 0,
                        UserStatus      = Status.Idle
                    }
                };
            }
            catch(Exception e) {
                this.LoginResult(LoginResult.ServersideError);
            }
        }

        private void HandlePacketData(Stream readStream) {
            using BanchoReader reader = new(readStream);

            PacketType packetType = (PacketType)reader.ReadUInt16();
            bool compressed = reader.ReadBoolean();
            int length = reader.ReadInt32();

            byte[] fullPacketBytes = reader.ReadBytes(length);

            using MemoryStream packetStream = new(fullPacketBytes);
            using BanchoReader packetReader = new(packetStream);

            Console.WriteLine($"got packet {packetType.ToString()}");

            switch (packetType) {
                case PacketType.OsuStatusUpdate: {
                    StatusUpdate update = new(packetStream);

                    this.UserStats.StatusUpdate = update;

                    this.SendOwnStats();
                    break;
                }
                case PacketType.OsuRequestStatusUpdate: {
                    this.SendOwnPresence();
                    this.SendOwnStats();
                    break;
                }
                case PacketType.OsuSendIrcMessage: {
                    Message message = new(packetStream);
                    message.GetChannel().SendMessage(this, message);
                    break;
                }
                case PacketType.OsuSpectateFrames: {
                    ReplayFrameBundle bundle = new(packetStream);

                    foreach (ClientOsu spectator in this.Spectators) {
                        spectator.SendSpectatorFrameBundle(bundle);
                    }

                    break;
                }
                case PacketType.OsuStartSpectating: {
                    Int userId = new(packetStream);

                    ClientOsu foundClient = ClientManager.ClientsByUserId.GetValueOrDefault(userId, null);

                    if (foundClient != null) {
                        this.SpectatingClient = foundClient;
                        this.SpectatingClient.StartSpectating(this);
                    }

                    break;
                }

                case PacketType.OsuStopSpectating: {
                    this.SpectatingClient?.StopSpectating(this);
                    this.SpectatingClient = null;

                    break;
                }

            }
        }

        #region Spectator

        public void StartSpectating(ClientOsu clientOsu) {
            this.SendSpectatorJoined(clientOsu);

            this.Spectators.Add(clientOsu);
        }

        public void StopSpectating(ClientOsu clientOsu) {
            this.SpectatorLeft(clientOsu);

            this.Spectators.Remove(clientOsu);
        }

        #endregion
    }
}
