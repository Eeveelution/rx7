using System.IO;
using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class UserStats : Serializable {
        public UserStats() {}
        public UserStats(Stream readStream) => this.ReadFromStream(readStream);
        [RetainDeclarationOrder] public int UserId          { get; set; }
        [RetainDeclarationOrder] public StatusUpdate StatusUpdate { get; set; }
        [RetainDeclarationOrder] public long RankedScore    { get; set; }
        [RetainDeclarationOrder] public float Accuracy      { get; set; }
        [RetainDeclarationOrder] public int Playcount       { get; set; }
        [RetainDeclarationOrder] public long TotalScore     { get; set; }
        [RetainDeclarationOrder] public int Rank            { get; set; }
    }
}
