using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets {
    public class BanchoSendIrcMessage : Serializable {
        [RetainDeclarationOrder] public Message Message { get; set; }

        public static implicit operator Message(BanchoSendIrcMessage message) => message.Message;
        public static implicit operator BanchoSendIrcMessage(Message message) => new() { Message = message };

    }

    public partial class ClientOsuPackets {
        public static void SendMessage(this ClientOsu clientOsu, BanchoSendIrcMessage message) {
            Packet<BanchoSendIrcMessage> messagePacket = new() {
                PacketId   = PacketType.BanchoSendIrcMessage,
                Compressed = false,
                PacketData = message
            };

            clientOsu.SendData(messagePacket.ToBytes());
        }
    }
}
