namespace _13B_REW.Bancho.Packets.Enums {
    public enum PacketType {
        OsuStatusUpdate             = 0,
        OsuQuit                     = 2,
        OsuRequestStatusUpdate      = 3,
        OsuPong                     = 4,
        OsuSendIrcMessagePrivate    = 25,
        OsuMatchNew                 = 31,
        OsuMatchJoin                = 32,
        OsuMatchChangeMods          = 51,
        OsuMatchPlayerFailed        = 56,
        OsuMatchSkipRequest         = 60,
        OsuChannelJoin              = 61,
        OsuBeatmapInfoRequest       = 68,
        OsuAddFriend                = 73,
        OsuRemoveFriend             = 74,
        OsuChannelLeave             = 78,
        OsuUserStatsRequest         = 85,
        OsuRecieveUserStatusUpdates = 79,

        BanchoLoginReply         = 5,
        BanchoSendIrcMessage     = 7,
        BanchoPing               = 8,
        BanchoUserPresence       = 83,
        BanchoUserUpdate         = 11,
        BanchoChannelJoinSuccess = 64,
    }
}
