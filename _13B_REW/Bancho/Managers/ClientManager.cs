using System.Collections.Concurrent;
using System.Collections.Generic;

namespace _13B_REW.Bancho.Managers {
    public class ClientManager {
        public static ConcurrentDictionary<int, ClientOsu>    ClientsByUserId   = new();
        public static ConcurrentDictionary<string, ClientOsu> ClientsByUsername = new();

        public static void RegisterClient(ClientOsu clientOsu) {
            string username = clientOsu.Username;
            int userId = clientOsu.UserId;

            if (ClientsByUsername.ContainsKey(username))
                ClientsByUsername.Remove(username, out _);

            ClientsByUsername.AddOrUpdate(username, clientOsu, null!);

            if (ClientsByUserId.ContainsKey(userId))
                ClientsByUserId.Remove(userId, out _);

            ClientsByUserId.AddOrUpdate(userId, clientOsu, null!);
        }

        public static void UnregisterClient(ClientOsu clientOsu) {
            string username = clientOsu.Username;
            int userId = clientOsu.UserId;

            ClientsByUsername.Remove(username, out _);
            ClientsByUserId.Remove(userId, out _);
        }
    }
}
