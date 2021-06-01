using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets.Chat {
    public class BanchoChannelAvailableAutojoin : Serializable {
        [RetainDeclarationOrder] public string ChannelName { get; set; }

        public static implicit operator BanchoChannelAvailableAutojoin(string channelName) => new() { ChannelName = channelName };
    }

    public static partial class ClientOsuPackets {
        public static void ChannelAvailableAutojoin(this ClientOsu clientOsu, string channelName) {
            Packet<BanchoChannelAvailableAutojoin> channelJoinSuccessPacket = new() {
                PacketId   = PacketType.BanchoChannelAvailableAutoJoin,
                Compressed = false,
                PacketData = channelName
            };

            clientOsu.SendData(channelJoinSuccessPacket.ToBytes());
        }
    }
}
