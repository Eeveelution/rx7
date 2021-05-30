using System.Collections.Concurrent;
using System.Collections.Generic;

namespace _13B_REW.Bancho.Managers {
    public class ClientManager {
        public static ConcurrentDictionary<int, ClientOsu>    ClientsByUserId   = new();
        public static ConcurrentDictionary<string, ClientOsu> ClientsByUsername = new();

        public static void RegisterClient(ClientOsu clientOsu) {
            string username = clientOsu.UserPresence.Username;
            int userId = clientOsu.UserPresence.UserId;

            if (ClientsByUsername.ContainsKey(username))
                ClientsByUsername.Remove(username, out _);

            ClientsByUsername.AddOrUpdate(username, clientOsu, null!);

            if (ClientsByUserId.ContainsKey(userId))
                ClientsByUserId.Remove(userId, out _);

            ClientsByUserId.AddOrUpdate(userId, clientOsu, null!);
        }
    }
}
