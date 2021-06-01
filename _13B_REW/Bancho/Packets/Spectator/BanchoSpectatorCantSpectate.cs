using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Spectator {
    public class BanchoSpectatorCantSpectate : Serializable {
        public BanchoSpectatorCantSpectate(){}
        public BanchoSpectatorCantSpectate(int userId) => this.Spectator = userId;
        [RetainDeclarationOrder] public int Spectator { get; set; }

        public static implicit operator BanchoSpectatorCantSpectate(int userId) => new() { Spectator = userId };
    }

    public static partial class ClientOsuPackets {
        public static void SpectatorCantSpectate(this ClientOsu clientOsu, BanchoSpectatorCantSpectate cantSpectate) {
            Packet<BanchoSpectatorCantSpectate> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorCantSpectate,
                Compressed = false,
                PacketData = cantSpectate
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SpectatorCantSpectate(this ClientOsu clientOsu, int spectatorId) {
            Packet<BanchoSpectatorCantSpectate> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorCantSpectate,
                Compressed = false,
                PacketData = spectatorId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SpectatorCantSpectate(this ClientOsu clientOsu, ClientOsu spectator) {
            Packet<BanchoSpectatorCantSpectate> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorCantSpectate,
                Compressed = false,
                PacketData = spectator.UserStats.UserId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
