using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoLoginResponse : Serializable {
        [RetainDeclarationOrder] public int UserId { get; set; }
    }
}
