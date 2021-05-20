using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets {
    public class BanchoUserStats : Serializable {
        [RetainDeclarationOrder] public int UserId          { get; set; }
        [RetainDeclarationOrder] public Status Status { get; set; }
        [RetainDeclarationOrder] public long RankedScore    { get; set; }
        [RetainDeclarationOrder] public float Accuracy      { get; set; }
        [RetainDeclarationOrder] public int Playcount       { get; set; }
        [RetainDeclarationOrder] public long TotalScore     { get; set; }
        [RetainDeclarationOrder] public int Rank            { get; set; }
    }
}
