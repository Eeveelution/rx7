using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Chat {
    public class BanchoChannelAvailable : Serializable {
        [RetainDeclarationOrder] public string ChannelName { get; set; }

        public static implicit operator BanchoChannelAvailable(string channelName) => new() { ChannelName = channelName };
    }

    public static partial class ClientOsuPackets {
        public static void ChannelAvailable(this ClientOsu clientOsu, string channelName) {
            Packet<BanchoChannelAvailable> channelJoinSuccessPacket = new() {
                PacketId   = PacketType.BanchoChannelAvailable,
                Compressed = false,
                PacketData = channelName
            };

            clientOsu.SendData(channelJoinSuccessPacket.ToBytes());
        }
    }
}
