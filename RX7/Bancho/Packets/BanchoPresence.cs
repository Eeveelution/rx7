using RX7.Bancho.Attributes;

namespace RX7.Bancho.Packets {
    public class BanchoPresence {
        [RetainDeclarationOrder] public int UserId          { get; set; }
        [RetainDeclarationOrder] public string Username { get; set; }
        [RetainDeclarationOrder] public byte AvatarExtension { get; set; }
        [RetainDeclarationOrder] public byte Timezone { get; set; }
        [RetainDeclarationOrder] public byte FuckingBasedValue { get; set; }
        [RetainDeclarationOrder] public string Location { get; set; }
        [RetainDeclarationOrder] public byte Permissions { get; set; }
        [RetainDeclarationOrder] public float Longnitude { get; set; }
        [RetainDeclarationOrder] public float Latitude { get; set; }
        [RetainDeclarationOrder] public int AnotherBasedValue { get; set; }
    }
}
