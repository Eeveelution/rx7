using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Packets {
    public class BanchoLoginResponse : Serializable {
        [RetainDeclarationOrder] public int UserId { get; set; }
    }

    public static partial class ClientOsuPackets {
        public static void LoginResult(this ClientOsu clientOsu, LoginResult result) {
            Packet<BanchoLoginResponse> loginPacket = new() {
                PacketId   = PacketType.BanchoLoginReply,
                Compressed = false,
                PacketData = new BanchoLoginResponse() {
                    UserId = (int) result
                }
            };

            clientOsu.SendData(loginPacket.ToBytes());
        }

        public static void LoginResult(this ClientOsu clientOsu, int userId) {
            Packet<BanchoLoginResponse> loginPacket = new() {
                PacketId   = PacketType.BanchoLoginReply,
                Compressed = false,
                PacketData = new BanchoLoginResponse() {
                    UserId = userId
                }
            };

            clientOsu.SendData(loginPacket.ToBytes());
        }
    }
}
