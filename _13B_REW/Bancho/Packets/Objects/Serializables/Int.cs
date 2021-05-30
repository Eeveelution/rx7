using System.IO;
using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class Int : Serializable {
        public Int(){}
        public Int(Stream stream) => this.ReadFromStream(stream);
        [RetainDeclarationOrder] public int Number { get; set; }

        public static implicit operator int(Int number) => number.Number;
        public static implicit operator Int(int number) => new() { Number = number };
    }
}
