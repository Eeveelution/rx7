using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Spectator {
    public class BanchoSpectatorLeft : Serializable {
        public BanchoSpectatorLeft(){}
        public BanchoSpectatorLeft(int userId) => this.Spectator = userId;
        [RetainDeclarationOrder] public int Spectator { get; set; }

        public static implicit operator BanchoSpectatorLeft(int userId) => new() { Spectator = userId };
    }

    public static partial class ClientOsuPackets {
        public static void SpectatorLeft(this ClientOsu clientOsu, BanchoSpectatorLeft spectatorLeft) {
            Packet<BanchoSpectatorLeft> annoucementPacket = new() {
                PacketId = PacketType.BanchoSpectatorLeft,
                Compressed = false,
                PacketData = spectatorLeft
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SpectatorLeft(this ClientOsu clientOsu, int spectatorId) {
            Packet<BanchoSpectatorLeft> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorLeft,
                Compressed = false,
                PacketData = spectatorId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SpectatorLeft(this ClientOsu clientOsu, ClientOsu spectator) {
            Packet<BanchoSpectatorLeft> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorLeft,
                Compressed = false,
                PacketData = spectator.UserStats.UserId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
