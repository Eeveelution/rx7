namespace _13B_REW.Bancho.Packets.Enums {
    public enum PacketType {
        OsuStatusUpdate             = 0,
        OsuSendIrcMessage           = 1,
        OsuQuit                     = 2,
        OsuRequestStatusUpdate      = 3,
        OsuPong                     = 4,
        OsuStartSpectating          = 16,
        OsuStopSpectating           = 17,
        OsuSpectateFrames           = 18,
        OsuCantSpectate             = 21,
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

        BanchoLoginReply            = 5,
        BanchoSendIrcMessage        = 7,
        BanchoPing                  = 8,
        BanchoSpectatorJoined       = 13,
        BanchoSpectatorLeft         = 14,
        BanchoSpectateFrames        = 15,
        BanchoClientUpdate          = 16,
        BanchoSpectatorCantSpectate = 22,
        BanchoAnnounce              = 24,
        BanchoLobbyJoin             = 29,
        BanchoLobbyPart             = 30,
        BanchoUserPresence          = 83,
        BanchoUserUpdate            = 11,
        BanchoChannelJoinSuccess    = 64,
    }
}
