using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class StatusUpdate : Serializable {
        [RetainDeclarationOrder] public byte UserStatus { get; set; }
        [RetainDeclarationOrder] public string Action { get; set; }
        [RetainDeclarationOrder] public string BeatmapChecksum { get; set; }
        [RetainDeclarationOrder] public ushort EnabledMods { get; set; }
        [RetainDeclarationOrder] public byte PlayMode { get; set; }
        [RetainDeclarationOrder] public int BeatmapId { get; set; }
    }
}
