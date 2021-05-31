using System;
using System.Collections.Concurrent;
using _13B_REW.Bancho.Managers;
using _13B_REW.Bancho.Packets;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.SchedulerJobs;
using EeveeTools.Servers.TCP;
using EeveeTools.Utilities.PollingJobScheduler;

namespace _13B_REW.Bancho {
    public static class Bancho {
        private static TcpServer             _tcpServer;
        private static AsyncPollingScheduler _scheduler = new();

        public static void InitializeBancho(string location, short port) {
            _tcpServer = new TcpServer(location, port, typeof(ClientOsu));

            ChannelManager.InitializeChannels();

            _scheduler.AddJob(new BanchoPinger());
            _scheduler.RunScheduler();

            Console.WriteLine("[Bancho] Bancho Initialized successfully...");
        }

        public static void BroadcastPacket(Action<ClientOsu> packetAction) {
            foreach (ClientOsu clientOsu in ClientManager.ClientList) {
                packetAction(clientOsu);
            }
        }

        public static void BroadcastPacketToOthers(Action<ClientOsu> packetAction, ClientOsu self) {
            foreach (ClientOsu clientOsu in ClientManager.ClientList) {
                if(clientOsu != self)
                    packetAction(clientOsu);
            }
        }

        public static void Start() {
            _tcpServer.Start();
        }
    }
}
