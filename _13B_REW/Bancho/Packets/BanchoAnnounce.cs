using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoAnnounce : Serializable {
        public BanchoAnnounce(){}
        public BanchoAnnounce(string announcement) => this.Announcement = announcement;
        [RetainDeclarationOrder] public string Announcement { get; set; }

        public static implicit operator BanchoAnnounce(string announcement) => new() { Announcement = announcement };
    }

    public partial class ClientOsuPackets {
        public static void SendAnnouncement(this ClientOsu clientOsu, BanchoAnnounce announce) {
            Packet<BanchoAnnounce> annoucementPacket = new() {
                PacketId = PacketType.BanchoAnnounce,
                Compressed = false,
                PacketData = announce
            };

            clientOsu.SendData(annoucementPacket.ToBytes());
        }
    }
}
