using System.Collections.Concurrent;
using System.Collections.Generic;
using EeveeTools.Servers.TCP;

namespace RX7.Bancho {
    public static class Bancho {
        private static TcpServer _tcpServer;

        public static ConcurrentDictionary<string, ClientOsu> ClientsByUsername;
        public static ConcurrentDictionary<int, ClientOsu>    ClientsByUserId;

        public static void InitializeBancho(string location, short port) {
            _tcpServer = new TcpServer(location, port, typeof(ClientOsu));
        }

        public static void RegisterClient(ClientOsu clientOsu) {
            //TODO: get stuff from ClientOsu to add
        }

        public static void Start() {
            _tcpServer.Start();
        }
    }
}
