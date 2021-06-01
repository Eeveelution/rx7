using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets {
    public class BanchoSpectatorFrames : Serializable {
        public BanchoSpectatorFrames(){}
        public BanchoSpectatorFrames(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder] public ReplayFrameBundle FrameBundle { get; set; }

        public static implicit operator ReplayFrameBundle(BanchoSpectatorFrames frames) => frames.FrameBundle;
        public static implicit operator BanchoSpectatorFrames(ReplayFrameBundle bundle) => new() { FrameBundle = bundle };
    }

    public static partial class ClientOsuPackets {
        public static void SpectateFrames(this ClientOsu clientOsu, BanchoSpectatorFrames bundle) {
            Packet<BanchoSpectatorFrames> frameBundlePacket = new() {
                PacketId   = PacketType.BanchoSpectateFrames,
                Compressed = false,
                PacketData = bundle
            };

            clientOsu.SendData(frameBundlePacket.ToBytes());
        }
    }
}
