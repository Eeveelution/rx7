using RX7.Bancho.Attributes;
using RX7.Bancho.Packets.Objects;

namespace RX7.Bancho.Packets {
    public class BanchoLoginResponse : Serializable {
        [RetainDeclarationOrder] public int UserId { get; set; }
    }
}
