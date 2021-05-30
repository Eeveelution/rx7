using System;

namespace _13B_REW.Bancho.Packets.Enums {
    [Flags]
    public enum ButtonState : byte {
        None   = 0,
        Left1  = 1,
        Right1 = 2,
        Left2  = 4,
        Right2 = 8,
    }
}
