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
using String=_13B_REW.Bancho.Packets.Objects.Serializables.String;

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

        #region TcpClientHandler Overrides

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
        protected override void HandleDisconnect() {
            this.Cleanup();
        }

        #endregion

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

                if (password != this.DatabaseUser.Password) {
                    this.LoginResult(LoginResult.WrongLogin);
                    return;
                }

                this.UserStats = new UserStats {
                    UserId      =         this.DatabaseUser.UserId,
                    Rank        = (int)   this.DatabaseUser.StandardRank,
                    Accuracy    = (float) this.DatabaseUser.StandardAccuracy / 100f,
                    Playcount   =         this.DatabaseUser.StandardPlaycount,
                    RankedScore =         this.DatabaseUser.StandardRankedScore,
                    StatusUpdate = new StatusUpdate() {
                        Action          = "User Just Logged in!",
                        BeatmapChecksum = "none",
                        BeatmapId       = -1,
                        Mods            = Mods.None,
                        PlayMode        = PlayModes.Osu,
                        UserStatus      = Status.Idle
                    }
                };

                this.UserPresence = new UserPresence {
                    UserId          = this.DatabaseUser.UserId,
                    Username        = this.DatabaseUser.Username,
                    Rank            = (int) this.DatabaseUser.StandardRank,
                    //TODO: avatar extension enum
                    AvatarExtension = 1,
                    //TODO: country list enum
                    Country         = 0,
                    //TODO: long and lat gathering
                    Latitude        = 0,
                    Longnitude      = 0,
                    Location        = this.DatabaseUser.Location,
                    //TODO: permissions
                    Permissions     = 0,
                    Timezone        = byte.Parse(timezone)
                };

                this.LoginResult(this.DatabaseUser.UserId);
                ChannelManager.Channels["#osu"]?.Join(this);

                Bancho.BroadcastPacket(osu => osu.UserStats(this.UserStats));
                Bancho.BroadcastPacket(osu => osu.UserPresence(this.UserPresence));
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
                    this.OwnStats();

                    Bancho.BroadcastPacket(osu => osu.UserPresence(this.UserPresence));
                    break;
                }
                case PacketType.OsuRequestStatusUpdate: {
                    this.OwnPresence();
                    this.OwnStats();
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
                        spectator.SpectateFrames(bundle);
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

                case PacketType.OsuQuit: {
                    this.Cleanup();
                    break;
                }

                case PacketType.OsuCantSpectate: {
                    this.SpectatingClient?.SpectatorCantSpectate(this);
                    break;
                }

                case PacketType.OsuSendIrcMessagePrivate: {
                    Message privateMessage = new(packetStream);

                    ClientOsu foundClient = ClientManager.ClientsByUsername.GetValueOrDefault(privateMessage.Target, null);
                    foundClient?.IrcMessage(privateMessage);

                    break;
                }

                case PacketType.OsuUserStatsRequest: {
                    ListInt users = new(packetStream);

                    foreach (int i in users.List) {
                        ClientOsu foundClient = ClientManager.ClientsByUserId.GetValueOrDefault(i, null);

                        if (foundClient != null) {
                            this.UserStats(foundClient.UserStats);
                            this.UserPresence(foundClient.UserPresence);
                        }
                    }

                    break;
                }

                case PacketType.OsuRecieveUserStatusUpdates: {
                    foreach (ClientOsu clientOsu in ClientManager.ClientsByUserId.Values) {
                        this.UserStats(clientOsu.UserStats);
                        this.UserPresence(clientOsu.UserPresence);
                    }

                    break;
                }

                case PacketType.OsuChannelLeave: {
                    String channelName = new(packetStream);

                    this.JoinedChannels.Find(channel => channel.GetName() == channelName)?.Part(this);

                    break;
                }

                case PacketType.OsuChannelJoin: {
                    String channelName = new(packetStream);
                    Channel foundChannel = ChannelManager.Channels.GetValueOrDefault(channelName, null);

                    if (foundChannel != null) {
                        foundChannel.Join(this);

                        this.JoinedChannels.Add(foundChannel);
                    }

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

        private void Cleanup() {
            this.SpectatingClient?.StopSpectating(this);
            this.SpectatingClient = null;

            foreach (Channel joinedChannel in this.JoinedChannels) {
                joinedChannel.Part(this);

                this.JoinedChannels.Remove(joinedChannel);
            }

            ClientManager.UnregisterClient(this);

            Bancho.BroadcastPacket(osu => osu.InformQuit(this.DatabaseUser.UserId));
        }
    }
}
