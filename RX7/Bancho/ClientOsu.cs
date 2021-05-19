using System.IO;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;
using RX7.Bancho.Objects.Serializables;

namespace RX7.Bancho {
    public class ClientOsu : TcpClientHandler {
        public BanchoStatus UserStatus = null;
        protected override void HandleData(byte[] data) {
            if (this.UserStatus == null) {
                StreamReader loginReader = new(new MemoryStream(data));

                string username = loginReader.ReadLine();
                string password = loginReader.ReadLine();
                string settings = loginReader.ReadLine();

                this.UserStatus = new() {
                    Action          = "User Just Logged in!",
                    BeatmapChecksum = "This man has not yet entered a Beatmap",
                    BeatmapId       = -1,
                    EnabledMods     = 0,
                    PlayMode        = 0,
                    Status          = 0
                };
            }

            using BanchoReader reader = new(new MemoryStream(data));

            ushort packetId = reader.ReadUInt16();
            bool compressed = reader.ReadBoolean();
            int length = reader.ReadInt32();

            byte[] fullPacketBytes = reader.ReadBytes(length);

            using BanchoReader packetReader = new(new MemoryStream(fullPacketBytes));

            switch (packetId) {}
        }
    }
}
