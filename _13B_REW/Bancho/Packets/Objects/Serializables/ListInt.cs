using System.Collections.Generic;
using System.IO;
using System.Linq;
using EeveeTools.Helpers;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class ListInt : Serializable {
        public ListInt() {}
        public ListInt(Stream readStream) => this.ReadFromStream(readStream);
        public ListInt(List<int> list) => this.List = list;
        public ListInt(IEnumerable<int> list) => this.List = list.ToList();

        private List<int> List = new();

        public override void ReadFromStream(Stream stream) {
            using BanchoReader reader = new(stream);

            int count = reader.ReadInt32();

            for(int i = 0; i != count; i++)
                this.List.Add(reader.ReadInt32());
        }

        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write(this.List.Count);

            for(int i = 0; i != this.List.Count; i++)
                writer.Write(this.List[i]);
        }
    }
}
