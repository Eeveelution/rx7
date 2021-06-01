using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Multiplayer {
    public partial class ClientOsuPackets {
        public static void MatchJoinFail(this ClientOsu clientOsu) {
            Packet packet = new() {
                PacketId   = PacketType.BanchoMatchJoinFail,
                Compressed = false,
                PacketData = null
            };

            clientOsu.SendData(packet.ToBytes());
        }
    }
}
