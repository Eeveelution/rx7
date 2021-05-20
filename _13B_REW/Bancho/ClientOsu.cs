using System;
using System.IO;
using _13B_REW.Bancho.Objects;
using _13B_REW.Bancho.Packets;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;
using EeveeTools.Helpers;
using EeveeTools.Servers.TCP;

namespace _13B_REW.Bancho {
    public class ClientOsu : TcpClientHandler {
        public UserStats         UserStats       = null;
        public UserPresence      UserPresence    = null;
        public ClientInformation UserInformation = null;
        protected override void HandleData(byte[] data) {
            if (this.UserStats == null || this.UserPresence == null || this.UserInformation == null) {
                StreamReader loginReader = new(new MemoryStream(data));

                string username = loginReader.ReadLine();
                string password = loginReader.ReadLine();
                string clientData = loginReader.ReadLine();

                this.HandleLogin(username, password, clientData);

                return;
            }

            using BanchoReader reader = new(new MemoryStream(data));

            PacketType packetType = (PacketType)reader.ReadUInt16();
            bool compressed = reader.ReadBoolean();
            int length = reader.ReadInt32();

            byte[] fullPacketBytes = reader.ReadBytes(length);

            using BanchoReader packetReader = new(new MemoryStream(fullPacketBytes));

            Console.WriteLine($"got packet {packetType.ToString()}");

            switch (packetType) {
                case PacketType.OsuRequestStatusUpdate:
                    this.SendOwnPresence();
                    this.SendOwnStats();
                    break;
            }
        }

        private void HandleLogin(string username, string password, string clientData) {
            try {
                string[] splitData = clientData?.Split("|");

                string version = splitData[0];
                string timezone = splitData[1];
                string showCityLocation = splitData[2];

                string multiaccountPreventionData = splitData[3];

                string[] splitNetworkData = multiaccountPreventionData.Split(":");

                this.UserInformation = new ClientInformation() {
                    ClientVersion         = version,
                    Timezone              = int.Parse(timezone),
                    CityLocationDisplayed = showCityLocation == "1",

                    HashedCommandLineArgs         = splitNetworkData[0],
                    PhysicalNetworkAdresses       = splitNetworkData[1].Split("."),
                    PhysicalNetworkAdressesHashed = splitNetworkData[2]
                };
            }
            catch {
                this.LoginResult(LoginResult.ServersideError);
            }
        }

        private void HandlePacketData(Stream readStream) {

        }
    }
}
