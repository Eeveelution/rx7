using System.IO;
using EeveeTools.Helpers;

namespace RX7.Bancho.Objects {
    public class Packet : Serializable {
        public ushort        PacketId;
        public bool          Compressed;
        public pSerializable PacketData;

        public Packet(Stream readStream) {
            using BanchoReader reader = new(readStream);

            this.PacketId   = reader.ReadUInt16();
            this.Compressed = reader.ReadBoolean();

            this.PacketData.ReadFromStream(readStream);
        }
        public override void ReadFromStream(Stream stream) {
            using BanchoReader reader = new(stream);

            this.PacketId   = reader.ReadUInt16();
            this.Compressed = reader.ReadBoolean();

            this.PacketData.ReadFromStream(stream);
        }
        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write(this.PacketId);
            writer.Write(this.Compressed);

            this.PacketData.WriteToStream(stream);
        }
    }
}
