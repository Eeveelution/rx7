using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets.User {
    public class BanchoUserStats : Serializable {
        [RetainDeclarationOrder] public UserStats Stats { get; set; }

        public static implicit operator UserStats(BanchoUserStats stats) => stats.Stats;
        public static implicit operator BanchoUserStats(UserStats stats) => new() { Stats = stats };
    }

    public static partial class ClientOsuPackets {
        public static void UserStats(this ClientOsu clientOsu, BanchoUserStats stats) {
            Packet<BanchoUserStats> statsPacket = new() {
                PacketId   = PacketType.BanchoUserUpdate,
                Compressed = false,
                PacketData = stats
            };

            clientOsu.SendData(statsPacket.ToBytes());
        }

        public static void OwnStats(this ClientOsu clientOsu) {
            Packet<BanchoUserStats> statsPacket = new() {
                PacketId   = PacketType.BanchoUserUpdate,
                Compressed = false,
                PacketData = clientOsu.UserStats
            };

            clientOsu.SendData(statsPacket.ToBytes());
        }
    }
}
