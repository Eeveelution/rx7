using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Chat {
    public class BanchoChannelRevoked : Serializable {
        [RetainDeclarationOrder] public string ChannelName { get; set; }

        public static implicit operator BanchoChannelRevoked(string channelName) => new() { ChannelName = channelName };
    }

    public static partial class ClientOsuPackets {
        public static void ChannelRevoked(this ClientOsu clientOsu, string channelName) {
            Packet<BanchoChannelRevoked> channelJoinSuccessPacket = new() {
                PacketId   = PacketType.BanchoChannelRevoked,
                Compressed = false,
                PacketData = channelName
            };

            clientOsu.SendData(channelJoinSuccessPacket.ToBytes());
        }
    }
}
