using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoUserQuit : Serializable {
        public BanchoUserQuit() {}
        public BanchoUserQuit(int userId) {}

        [RetainDeclarationOrder] public int UserId { get; set; }

        public static implicit operator BanchoUserQuit(int userId) => new() { UserId = userId };
    }

    public static partial class ClientOsuPackets {
        public static void InformQuit(this ClientOsu clientOsu, int userId) {
            Packet<BanchoUserQuit> userQuit = new() {
                PacketId   = PacketType.BanchoUserQuit,
                Compressed = false,
                PacketData = userId
            };

            clientOsu.SendData(userQuit.ToBytes());
        }

        public static void InformQuit(this ClientOsu clientOsu, ClientOsu quitOsu) {
            Packet<BanchoUserQuit> userQuit = new() {
                PacketId   = PacketType.BanchoUserQuit,
                Compressed = false,
                PacketData = quitOsu.DatabaseUser.UserId
            };

            clientOsu.SendData(userQuit.ToBytes());
        }
    }
}
