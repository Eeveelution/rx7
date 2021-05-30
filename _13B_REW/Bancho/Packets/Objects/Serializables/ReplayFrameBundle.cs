using System.IO;
using EeveeTools.Helpers;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    /// TODO:
    ///
    /// Vulnerabilities:
    ///     sending Uint16.MaxValue FrameCount could potentially be used for Lagging the Server
    ///
    public class ReplayFrameBundle : Serializable {
        public int           FrameCount;
        public ReplayFrame[] Frames;
        //TODO: enum
        public byte          ReplayAction;
        public ScoreFrame    ReplayScoreFrame;

        public ReplayFrameBundle() {}
        public ReplayFrameBundle(Stream readStream) => this.ReadFromStream(readStream);

        public override void ReadFromStream(Stream stream) {
            using BanchoReader reader = new(stream);

            this.FrameCount = reader.ReadUInt16();
            this.Frames     = new ReplayFrame[this.FrameCount];

            for (int i = 0; i != this.FrameCount; i++) {
                this.Frames[0] = new ReplayFrame(stream);
            }

            this.ReplayAction     = reader.ReadByte();
            this.ReplayScoreFrame = new ScoreFrame(stream);
        }

        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write((ushort)this.FrameCount);

            foreach (ReplayFrame replayFrame in this.Frames) {
                replayFrame.WriteToStream(stream);
            }

            writer.Write(this.ReplayAction);

            this.ReplayScoreFrame.WriteToStream(stream);
        }
    }
}
