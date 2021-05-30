using System;
using System.IO;
using _13B_REW.Bancho.Packets.Enums;
using EeveeTools.Helpers;

namespace _13B_REW.Bancho.Packets.Objects {
    public class Packet<pSerializable> : Serializable where pSerializable : Serializable {
        public PacketType    PacketId;
        public bool          Compressed;
        public pSerializable PacketData;
        public Packet() {}
        public override void ReadFromStream(Stream stream) {
            throw new NotImplementedException();
        }
        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write((short)this.PacketId);
            writer.Write(this.Compressed);

            if (this.PacketData != null) {
                byte[] packetData = this.PacketData.ToBytes();

                writer.Write(packetData.Length);

                if (packetData.Length != 0)
                    writer.Write(packetData);
            } else {
                writer.Write(0);
            }
        }
    }

    public class Packet : Packet<Serializable> { }
}
