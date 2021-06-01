using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoLobbyJoin : Serializable {
        public BanchoLobbyJoin(){}
        public BanchoLobbyJoin(int userId) => this.User = userId;
        [RetainDeclarationOrder] public int User { get; set; }

        public static implicit operator BanchoLobbyJoin(int userId) => new() { User = userId };
    }

    public static partial class ClientOsuPackets {
        public static void InformLobbyJoin(this ClientOsu clientOsu, BanchoLobbyJoin lobbyJoin) {
            Packet<BanchoLobbyJoin> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyJoin,
                Compressed = false,
                PacketData = lobbyJoin
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void InformLobbyJoin(this ClientOsu clientOsu, int userId) {
            Packet<BanchoLobbyJoin> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyJoin,
                Compressed = false,
                PacketData = userId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void InformLobbyJoin(this ClientOsu clientOsu, ClientOsu user) {
            Packet<BanchoLobbyJoin> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyJoin,
                Compressed = false,
                PacketData = user.UserStats.UserId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
