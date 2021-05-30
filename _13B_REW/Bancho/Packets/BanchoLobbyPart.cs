using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoLobbyPart : Serializable {
        public BanchoLobbyPart(){}
        public BanchoLobbyPart(int userId) => this.User = userId;
        [RetainDeclarationOrder] public int User { get; set; }

        public static implicit operator BanchoLobbyPart(int userId) => new() { User = userId };
    }

    public static partial class ClientOsuPackets {
        public static void InformLobbyPart(this ClientOsu clientOsu, BanchoLobbyPart lobbyPart) {
            Packet<BanchoLobbyPart> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyPart,
                Compressed = false,
                PacketData = lobbyPart
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void InformLobbyPart(this ClientOsu clientOsu, int userId) {
            Packet<BanchoLobbyPart> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyPart,
                Compressed = false,
                PacketData = userId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void InformLobbyPart(this ClientOsu clientOsu, ClientOsu user) {
            Packet<BanchoLobbyPart> annoucementPacket = new() {
                PacketId   = PacketType.BanchoLobbyPart,
                Compressed = false,
                PacketData = user.UserStats.UserId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
