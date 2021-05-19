using System;
using System.IO;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;
using RX7.Bancho.Objects;
using RX7.Bancho.Objects.Serializables;
using RX7.Bancho.Packets;

namespace RX7.Bancho {
    public class ClientOsu : TcpClientHandler {
        public BanchoUserStats UserStats    = null;
        public BanchoPresence  UserPresence = null;
        protected override void HandleData(byte[] data) {
            Console.WriteLine("got data");

            if (this.UserStats == null || this.UserPresence == null) {
                StreamReader loginReader = new(new MemoryStream(data));

                string username = loginReader.ReadLine();
                string password = loginReader.ReadLine();
                string settings = loginReader.ReadLine();

                this.UserStats = new BanchoUserStats() {
                    UserId      = 24,
                    RankedScore = 123123,
                    TotalScore  = 234324,
                    Accuracy    = 0.99999f,
                    Playcount   = 918273,
                    Rank        = 12,
                    Status = new Status() {
                        Action          = "Litearlly nothing",
                        BeatmapChecksum = "nothing...",
                        BeatmapId       = 123213213,
                        EnabledMods     = 0,
                        PlayMode        = 0,
                        UserStatus      = 1
                    }
                };

                this.UserPresence = new BanchoPresence() {
                    Username          = username,
                    AvatarExtension   = 0,
                    FuckingBasedValue = 123,
                    Latitude          = 0.5f,
                    Longnitude        = 12f,
                    Location          = "kurwa",
                    Permissions       = 1,
                    Timezone          = 24,
                    UserId            = 24,
                    AnotherBasedValue = 0,
                };

                this.LoginResult(24);
            }

            using BanchoReader reader = new(new MemoryStream(data));

            ushort packetId = reader.ReadUInt16();
            bool compressed = reader.ReadBoolean();
            int length = reader.ReadInt32();

            byte[] fullPacketBytes = reader.ReadBytes(length);

            using BanchoReader packetReader = new(new MemoryStream(fullPacketBytes));

            switch (packetId) {}
        }

        #region Packets

        private void LoginResult(int userId) {
            Packet<BanchoLoginResponse> loginPacket = new() {
                PacketId   = 5,
                Compressed = false,
                PacketData = new BanchoLoginResponse() {
                    UserId = userId
                }
            };

            this.SendData(loginPacket.ToBytes());
        }

        #endregion
    }
}
