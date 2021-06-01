using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets.Chat {
    public class BanchoSendIrcMessage : Serializable {
        [RetainDeclarationOrder] public Message Message { get; set; }

        public static implicit operator Message(BanchoSendIrcMessage message) => message.Message;
        public static implicit operator BanchoSendIrcMessage(Message message) => new() { Message = message };

    }

    public static partial class ClientOsuPackets {
        public static void IrcMessage(this ClientOsu clientOsu, BanchoSendIrcMessage message) {
            Packet<BanchoSendIrcMessage> messagePacket = new() {
                PacketId   = PacketType.BanchoSendIrcMessage,
                Compressed = false,
                PacketData = message
            };

            clientOsu.SendData(messagePacket.ToBytes());
        }

        public static void RotaryBotMessageOsu(this ClientOsu clientOsu, string message) {
            Packet<BanchoSendIrcMessage> messagePacket = new() {
                PacketId   = PacketType.BanchoSendIrcMessage,
                Compressed = false,
                PacketData = new Message("RotaryBot", message, "#osu")
            };

            clientOsu.SendData(messagePacket.ToBytes());
        }

        public static void RotaryBotMessagePrivate(this ClientOsu clientOsu, string message) {
            Packet<BanchoSendIrcMessage> messagePacket = new() {
                PacketId   = PacketType.BanchoSendIrcMessage,
                Compressed = false,
                PacketData = new Message("RotaryBot", message, clientOsu.Username)
            };

            clientOsu.SendData(messagePacket.ToBytes());
        }
    }
}
