using System;
using System.IO;
using EeveeTools.Helpers;

namespace RX7.Bancho.Objects {
    public class Packet<pSerializable> : Serializable where pSerializable : Serializable {
        public ushort        PacketId;
        public bool          Compressed;
        public pSerializable PacketData;

        public Packet(pSerializable packetData) { this.PacketData = packetData; }
        public Packet() {}
        public override void ReadFromStream(Stream stream) {
            throw new NotImplementedException();
        }
        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write(this.PacketId);
            writer.Write(this.Compressed);

            byte[] packetData = this.PacketData.ToBytes();

            writer.Write(packetData.Length);

            writer.Write(packetData);
        }
    }
}
