using System.IO;
using RX7.Bancho.Attributes;
using RX7.Bancho.Packets.Objects;
using RX7.Bancho.Packets.Objects.Serializables;

namespace RX7.Bancho.Packets {
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
