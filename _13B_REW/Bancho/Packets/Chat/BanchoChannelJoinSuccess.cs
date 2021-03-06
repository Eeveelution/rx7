using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Chat {
    public class BanchoChannelJoinSuccess : Serializable {
        [RetainDeclarationOrder] public string ChannelName { get; set; }

        public static implicit operator BanchoChannelJoinSuccess(string channelName) => new() { ChannelName = channelName };
    }

    public static partial class ClientOsuPackets {
        public static void ChannelJoinSuccess(this ClientOsu clientOsu, string channelName) {
            Packet<BanchoChannelJoinSuccess> channelJoinSuccessPacket = new() {
                PacketId   = PacketType.BanchoChannelJoinSuccess,
                Compressed = false,
                PacketData = channelName
            };

            clientOsu.SendData(channelJoinSuccessPacket.ToBytes());
        }
    }
}
