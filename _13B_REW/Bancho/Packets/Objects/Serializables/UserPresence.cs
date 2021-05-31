using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class UserPresence : Serializable {
        public UserPresence() {}
        public UserPresence(Stream readStream) => this.ReadFromStream(readStream);
        [RetainDeclarationOrder] public int UserId          { get; set; }
        [RetainDeclarationOrder] public string Username { get; set; }
        [RetainDeclarationOrder] public AvatarExtension AvatarExtension { get; set; }
        [RetainDeclarationOrder] public byte Timezone { get; set; }
        [RetainDeclarationOrder] public byte Country { get; set; }
        [RetainDeclarationOrder] public string Location { get; set; }
        [RetainDeclarationOrder] public byte Permissions { get; set; }
        [RetainDeclarationOrder] public float Longnitude { get; set; }
        [RetainDeclarationOrder] public float Latitude { get; set; }
        [RetainDeclarationOrder] public int Rank { get; set; }
    }
}
