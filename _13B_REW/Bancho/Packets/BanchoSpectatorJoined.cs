using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoSpectatorJoined : Serializable {
        public BanchoSpectatorJoined(){}
        public BanchoSpectatorJoined(int userId) => this.Spectator = userId;
        [RetainDeclarationOrder] public int Spectator { get; set; }

        public static implicit operator BanchoSpectatorJoined(int userId) => new() { Spectator = userId };
    }

    public partial class ClientOsuPackets {
        public static void SendSpectatorJoined(this ClientOsu clientOsu, BanchoSpectatorJoined spectatorJoined) {
            Packet<BanchoSpectatorJoined> annoucementPacket = new() {
                PacketId = PacketType.BanchoSpectatorJoined,
                Compressed = false,
                PacketData = spectatorJoined
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SendSpectatorJoined(this ClientOsu clientOsu, int spectatorId) {
            Packet<BanchoSpectatorJoined> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorJoined,
                Compressed = false,
                PacketData = spectatorId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }

        public static void SendSpectatorJoined(this ClientOsu clientOsu, ClientOsu spectator) {
            Packet<BanchoSpectatorJoined> annoucementPacket = new() {
                PacketId   = PacketType.BanchoSpectatorJoined,
                Compressed = false,
                PacketData = spectator.UserStats.UserId
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
