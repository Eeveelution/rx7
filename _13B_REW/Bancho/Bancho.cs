using System.Collections.Concurrent;
using System.Collections.Generic;
using EeveeTools.Servers.TCP;

namespace _13B_REW.Bancho {
    public static class Bancho {
        private static TcpServer _tcpServer;

        public static ConcurrentDictionary<string, ClientOsu> ClientsByUsername;
        public static ConcurrentDictionary<int, ClientOsu>    ClientsByUserId;

        public static void InitializeBancho(string location, short port) {
            _tcpServer = new TcpServer(location, port, typeof(ClientOsu));
        }

        public static void RegisterClient(ClientOsu clientOsu) {
            string username = clientOsu.UserPresence.Username;
            int userId = clientOsu.UserPresence.UserId;

            if (!ClientsByUsername.ContainsKey(username))
                ClientsByUsername.AddOrUpdate(username, clientOsu, null!);

            if (!ClientsByUserId.ContainsKey(userId))
                ClientsByUserId.AddOrUpdate(userId, clientOsu, null!);
        }

        public static void Start() {
            _tcpServer.Start();
        }
    }
}
