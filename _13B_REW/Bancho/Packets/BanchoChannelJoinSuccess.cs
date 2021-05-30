using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;

namespace _13B_REW.Bancho.Packets.Objects {
    public class BanchoChannelJoinSuccess : Serializable {
        [RetainDeclarationOrderAttribute] public string ChannelName { get; set; }

        public static implicit operator BanchoChannelJoinSuccess(string channelName) => new() { ChannelName = channelName };
    }

    public static partial class ClientOsuPackets {
        public static void SendJoinSuccess(this ClientOsu clientOsu, string channelName) {
            Packet<BanchoChannelJoinSuccess> channelJoinSuccessPacket = new() {
                PacketId   = PacketType.BanchoChannelJoinSuccess,
                Compressed = false,
                PacketData = channelName
            };

            clientOsu.SendData(channelJoinSuccessPacket.ToBytes());
        }
    }
}
