using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Managers {
    public class MultiplayerManager {
        public static ConcurrentDictionary<ushort, ServersideMatch> MatchesById = new();
        public static IEnumerable<ServersideMatch> MatchList => MatchesById.Values;
    }
}
