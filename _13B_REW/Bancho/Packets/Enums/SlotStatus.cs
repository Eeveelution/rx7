namespace _13B_REW.Bancho.Packets.Enums {
    public enum SlotStatus : byte {
        Open          = 1,
        Locked        = 2,
        NotReady      = 4,
        Ready         = 8,
        NoMap         = 16,
        Playing       = 32,
        Complete      = 64,
        CompHasPlayer = 124,
        Quit          = 128,
    }
}
