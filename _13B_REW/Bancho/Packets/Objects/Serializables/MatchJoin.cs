using System.IO;
using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class MatchJoin : Serializable {
        public MatchJoin() {}
        public MatchJoin(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder] public int MatchId { get; set; }
        [RetainDeclarationOrder] public string Password { get; set; }
    }
}
