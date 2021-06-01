using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Other {
    public static partial class ClientOsuPackets {
        public static void Ping(this ClientOsu clientOsu) {
            Packet packet = new() {
                PacketId   = PacketType.BanchoPing,
                Compressed = false,
                PacketData = null
            };

            clientOsu.SendData(packet.ToBytes());
        }
    }
}
