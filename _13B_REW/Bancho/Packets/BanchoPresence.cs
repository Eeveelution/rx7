using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets {
    public class BanchoPresence : Serializable {
        [RetainDeclarationOrder] public UserPresence Presence { get; set; }

        public static implicit operator UserPresence(BanchoPresence presence) => presence.Presence;
        public static implicit operator BanchoPresence(UserPresence presence) => new() { Presence = presence };
    }

    public partial class ClientOsuPackets {
        public static void SendUserPresence(this ClientOsu clientOsu, BanchoPresence presence) {
            Packet<BanchoPresence> presencePacket = new() {
                PacketId   = PacketType.BanchoUserPresence,
                Compressed = false,
                PacketData = presence
            };

            clientOsu.SendData(presencePacket.ToBytes());
        }

        public static void SendOwnPresence(this ClientOsu clientOsu) {
            Packet<BanchoPresence> presencePacket = new() {
                PacketId   = PacketType.BanchoUserPresence,
                Compressed = false,
                PacketData = clientOsu.UserPresence
            };

            clientOsu.SendData(presencePacket.ToBytes());
        }
    }
}
