using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets.Other {
    public static partial class ClientOsuPackets {
        public static void SetMultiplayerFlags(this ClientOsu clientOsu) {
            Packet<Int> flagPacket = new() {
                PacketId   = PacketType.BanchoMultiplayerFlagThing,
                Compressed = false,
                PacketData = 5
            };

            clientOsu.SendData(flagPacket.ToBytes());
        }
    }
}
