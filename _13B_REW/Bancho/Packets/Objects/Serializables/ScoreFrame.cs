using System.IO;
using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class ScoreFrame : Serializable {
        public ScoreFrame(){}
        public ScoreFrame(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder] public int Time { get; set; }
        [RetainDeclarationOrder] public byte Id { get; set; }
        [RetainDeclarationOrder] public ushort Count300 { get; set; }
        [RetainDeclarationOrder] public ushort Count100 { get; set; }
        [RetainDeclarationOrder] public ushort Count50 { get; set; }
        [RetainDeclarationOrder] public ushort CountGeki { get; set; }
        [RetainDeclarationOrder] public ushort CountKatu { get; set; }
        [RetainDeclarationOrder] public ushort CountMiss { get; set; }
        [RetainDeclarationOrder] public int TotalScore { get; set; }
        [RetainDeclarationOrder] public ushort MaxCombo { get; set; }
        [RetainDeclarationOrder] public ushort CurrentCombo { get; set; }
        [RetainDeclarationOrder] public bool Perfect { get; set; }
        [RetainDeclarationOrder] public byte CurrentHp { get; set; }
        [RetainDeclarationOrder] public byte TagByte { get; set; }
    }
}
