namespace _13B_REW.Bancho.Packets.Enums {
    public enum PacketType {
        /// <summary>
        /// osu! Has Changed their Status and what it's doing
        /// </summary>
        OsuStatusUpdate = 0,
        /// <summary>
        /// osu! has sent a chat message
        /// </summary>
        OsuSendIrcMessage = 1,
        /// <summary>
        /// osu! has left the game
        /// </summary>
        OsuQuit = 2,
        /// <summary>
        /// osu! is requesting it's own stats
        /// </summary>
        OsuRequestStatusUpdate = 3,
        /// <summary>
        /// osu! is Ponging back
        /// </summary>
        OsuPong = 4,
        /// <summary>
        /// osu! has begun spectating someone
        /// </summary>
        OsuStartSpectating = 16,
        /// <summary>
        /// osu! has stopped spectating someone
        /// </summary>
        OsuStopSpectating = 17,
        /// <summary>
        /// osu! is sending Replay Frames because it has spectators
        /// </summary>
        OsuSpectateFrames = 18,
        /// <summary>
        /// osu! is informing Bancho that it doesn't own the Beatmap that the user that it's spectating is playing
        /// </summary>
        OsuCantSpectate = 21,
        /// <summary>
        /// osu! is sending a Private Message to someone
        /// </summary>
        OsuSendIrcMessagePrivate = 25,
        /// <summary>
        /// osu! is Creating a Multiplayer match
        /// </summary>
        OsuMatchNew = 31,
        /// <summary>
        /// osu! is attempting to Join a match
        /// </summary>
        OsuMatchJoin = 32,
        /// <summary>
        /// osu! has changed mods in the match it's in
        /// </summary>
        OsuMatchChangeMods = 51,
        /// <summary>
        /// osu! is Informing bancho that it has failed
        /// </summary>
        OsuMatchPlayerFailed = 56,
        /// <summary>
        /// osu! is Informing Bancho that it wants to skip the beginning break/end
        /// </summary>
        OsuMatchSkipRequest = 60,
        /// <summary>
        /// osu! is requesting to join a Chat Channel
        /// </summary>
        OsuChannelJoin = 63,
        /// <summary>
        /// osu! is requesting more Information about some Beatmaps
        /// </summary>
        OsuBeatmapInfoRequest = 68,
        /// <summary>
        /// osu! added a user to their friends list
        /// </summary>
        OsuAddFriend = 73,
        /// <summary>
        /// osu! has removed a user from their friends list
        /// </summary>
        OsuRemoveFriend = 74,
        /// <summary>
        /// osu! has left a chat channel
        /// </summary>
        OsuChannelLeave = 78,
        /// <summary>
        /// osu! is requesting a specific users presence
        /// </summary>
        OsuUserStatsRequest = 85,
        /// <summary>
        /// osu! is requesting user presences for other online players
        /// </summary>
        OsuRecieveUserStatusUpdates = 79,


        /// <summary>
        /// Bancho is telling osu! whether it has succeeded in Logging in or not
        /// </summary>
        BanchoLoginReply = 5,
        /// <summary>
        /// Bancho is telling osu! that a chat message has been sent
        /// </summary>
        BanchoSendIrcMessage = 7,
        /// <summary>
        /// Bancho is checking in on osu! to see if it's still there
        /// </summary>
        BanchoPing = 8,
        /// <summary>
        /// Bancho is telling osu! that a user has left Bancho
        /// </summary>
        BanchoUserQuit = 12,
        /// <summary>
        /// Bancho is telling osu! that someone has decided to spectate it
        /// </summary>
        BanchoSpectatorJoined = 13,
        /// <summary>
        /// Bancho is telling osu! that a spectator has left
        /// </summary>
        BanchoSpectatorLeft = 14,
        /// <summary>
        /// Bancho is giving osu! Replay Frames to play back
        /// </summary>
        BanchoSpectateFrames = 15,
        /// <summary>
        /// Bancho is informing osu! that a client update has just been released
        /// </summary>
        BanchoClientUpdate = 19,
        /// <summary>
        /// Bancho is telling osu! that a certain spectator has failed to spectate
        /// </summary>
        BanchoSpectatorCantSpectate = 22,
        /// <summary>
        /// whatever the fuck this is:
        ///
        /// case PacketType.const_23:
        ///     Class70.bool_6 = true;
        ///     break;
        ///
        /// </summary>
        BanchoMultiplayerFlagThing = 23,
        /// <summary>
        /// Bancho is sending a notification to osu!
        /// </summary>
        BanchoAnnounce = 24,
        /// <summary>
        /// Bancho is informing osu! that a certain user has joined the Multiplayer Lobby
        /// </summary>
        BanchoLobbyJoin = 30,
        /// <summary>
        /// Bancho is informing osu! that a certain user has left the Multiplayer Lobby
        /// </summary>
        BanchoLobbyPart = 29,
        /// <summary>
        /// Bancho is giving osu! a certain users Presence Information
        /// </summary>
        BanchoUserPresence = 83,
        /// <summary>
        /// Bancho is giving osu! a certain users Stats Information (Rank, Score, Playmode, Playcount)
        /// </summary>
        BanchoUserUpdate = 11,
        /// <summary>
        /// Bancho is telling osu! it has succeeded in joining a channel
        /// </summary>
        BanchoChannelJoinSuccess = 64,
        /// <summary>
        /// Bancho is telling osu! that another chat channel is available to join
        /// </summary>
        BanchoChannelAvailable         = 65,
        /// <summary>
        /// Bancho is telling osu! that a chat channel has been removed
        /// </summary>
        BanchoChannelRevoked           = 66,
        /// <summary>
        /// Bancho is telling osu! that a chat channel is available to join and to join it automatically
        /// </summary>
        BanchoChannelAvailableAutoJoin = 67,
    }
}
