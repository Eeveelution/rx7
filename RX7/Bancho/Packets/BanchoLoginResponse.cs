using RX7.Bancho.Attributes;
using RX7.Bancho.Objects;

namespace RX7.Bancho.Packets {
    public class BanchoLoginResponse : Serializable {
        [RetainDeclarationOrder] public int UserId { get; set; }
    }
}
