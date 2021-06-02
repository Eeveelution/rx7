using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Packets.Chat;
using _13B_REW.Bancho.Packets.Multiplayer;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Managers {
    public class MultiplayerManager {
        public static ConcurrentDictionary<ushort, ServersideMatch> MatchesById = new();
        public static IEnumerable<ServersideMatch> MatchList => MatchesById.Values;

        public static bool MatchNew(ClientOsu clientOsu, Match createdMatch) {
            createdMatch.MatchId = GetFreeId();

            if (createdMatch.MatchId != ushort.MaxValue) {
                ServersideMatch match = new(clientOsu, createdMatch);

                MatchesById.TryAdd(createdMatch.MatchId, match);

                clientOsu.CurrentMultiplayerMatch = match;

                clientOsu.ChannelAvailableAutojoin("#multiplayer");
                clientOsu.MatchJoinSuccess(match);

                BroadcastMatchUpdates();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a Free Match ID for Shiro Multiplayer Rooms
        /// </summary>
        /// <returns></returns>
        private static ushort GetFreeId() {
            for (ushort i = 0; i != 256; i++)
                if (!MatchesById.ContainsKey(i))
                    return i;
            return ushort.MaxValue;
        }

        public static void BroadcastMatchUpdates() {
            foreach (ServersideMatch serversideMatch in MatchList)
                Bancho.BroadcastPacket(client => client.MatchUpdate(serversideMatch));
        }
    }
}
