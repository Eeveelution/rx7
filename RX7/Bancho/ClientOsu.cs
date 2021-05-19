using System.IO;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;

namespace RX7.Bancho {
    public class ClientOsu : TcpClientHandler {

        protected override void HandleData(byte[] data) {
            using BanchoReader reader = new(new MemoryStream(data));

            ushort packetId = reader.ReadUInt16();
            bool compressed = reader.ReadBoolean();
            int length = reader.ReadInt32();

            byte[] fullPacketBytes = reader.ReadBytes(length);

            using BanchoReader packetReader = new(new MemoryStream(fullPacketBytes));

            switch (packetId) {
                
            }
        }
    }
}
