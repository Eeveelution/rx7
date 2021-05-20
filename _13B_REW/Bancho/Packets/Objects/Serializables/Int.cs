using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class Int : Serializable {
        [RetainDeclarationOrder] public int Number { get; set; }

        public static implicit operator int(Int number) => number.Number;
        public static implicit operator Int(int number) => new() { Number = number };
    }
}
