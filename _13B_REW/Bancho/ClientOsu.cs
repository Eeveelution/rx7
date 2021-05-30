using System;
using System.Collections.Generic;
using System.IO;
using _13B_REW.Bancho.Managers;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Objects;
using _13B_REW.Bancho.Packets;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects.Serializables;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;

namespace _13B_REW.Bancho {
    public class ClientOsu : TcpClientHandler {
        public UserStats         UserStats       = null;
        public UserPresence      UserPresence    = null;
        public ClientInformation UserInformation = null;

        public readonly List<Channel> JoinedChannels = new();

        public readonly  List<ClientOsu> Spectators       = new();
        private readonly object          _spectatorLock   = new();
        public           ClientOsu       SpectatingClient = null;
        protected override void HandleData(byte[] data) {
            if (this.UserStats == null || this.UserPresence == null || this.UserInformation == null) {
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

                //TODO: db

                this.UserStats = new UserStats() {
                    UserId      = 24,
                    RankedScore = 12123,
                    TotalScore  = 12397,
                    Accuracy    = 0.9123f,
                    Playcount   = 123,
                    Rank        = 1,
                    StatusUpdate = new StatusUpdate() {
                        Action          = "chilling",
                        BeatmapChecksum = "none",
                        BeatmapId       = 123,
                        EnabledMods     = 1231,
                        PlayMode        = 2,
                        UserStatus      = Status.Lobby
                    }
                };

                this.UserPresence = new UserPresence() {
                    AvatarExtension   = 1,
                    AnotherBasedValue = 2,
                    FuckingBasedValue = 21,
                    Latitude          = 12.42f,
                    Location          = "eevees home",
                    Longnitude        = 123f,
                    Permissions       = 3,
                    Timezone          = 3,
                    UserId            = 24,
                    Username          = "Eevee"
                };

                this.LoginResult(24);

                this.SendJoinSuccess("#osu");

                this.SendMessage(new Message() {Sender = "Mazda", Target = "#osu", Text = "Hello there"});
            }
            catch {
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
