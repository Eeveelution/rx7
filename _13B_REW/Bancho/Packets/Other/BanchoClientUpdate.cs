using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public static partial class ClientOsuPackets {
        public static void ClientUpdate(this ClientOsu clientOsu) {
            Packet packet = new() {
                PacketId   = PacketType.BanchoClientUpdate,
                Compressed = false,
                PacketData = null
            };
        }
    }
}
