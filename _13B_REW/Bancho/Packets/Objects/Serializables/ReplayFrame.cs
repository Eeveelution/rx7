using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class ReplayFrame : Serializable {
        public ReplayFrame() {}
        public ReplayFrame(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder] public ButtonState ButtonState { get; set; }
        [RetainDeclarationOrder] public int OldTaikoCompatibilityFlagThingUnused { get; set; } = 0;
        [RetainDeclarationOrder] public float MouseX { get; set; }
        [RetainDeclarationOrder] public float MouseY { get; set; }
        [RetainDeclarationOrder] public int Time { get; set; }
    }
}
