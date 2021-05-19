using RX7.Bancho.Attributes;

namespace RX7.Bancho.Objects.Serializables {
    public class BanchoStatus : Serializable {
        [RetainDeclarationOrder] public byte Status { get; set; }
        [RetainDeclarationOrder] public string Action { get; set; }
        [RetainDeclarationOrder] public string BeatmapChecksum { get; set; }
        [RetainDeclarationOrder] public ushort EnabledMods { get; set; }
        [RetainDeclarationOrder] public byte PlayMode { get; set; }
        [RetainDeclarationOrder] public int BeatmapId { get; set; }
    }
}
