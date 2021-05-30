using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class StatusUpdate : Serializable {
        public StatusUpdate() {}
        public StatusUpdate(Stream readStream) => this.ReadFromStream(readStream);
        [RetainDeclarationOrder] public Status UserStatus { get; set; }
        [RetainDeclarationOrder] public string Action { get; set; }
        [RetainDeclarationOrder] public string BeatmapChecksum { get; set; }
        [RetainDeclarationOrder] public Mods Mods { get; set; }
        [RetainDeclarationOrder] public PlayModes PlayMode { get; set; }
        [RetainDeclarationOrder] public int BeatmapId { get; set; }
    }
}
