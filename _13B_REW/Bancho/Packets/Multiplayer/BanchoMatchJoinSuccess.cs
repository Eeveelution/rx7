using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets.Multiplayer {
    public class BanchoMatchJoinSuccess : Serializable {
        public BanchoMatchJoinSuccess() {}
        public BanchoMatchJoinSuccess(Match match) => this.Match = match;
        public BanchoMatchJoinSuccess(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder]
        public Match Match { get; set; }

        public static implicit operator Match(BanchoMatchJoinSuccess matchUpdate) => matchUpdate.Match;
        public static implicit operator BanchoMatchJoinSuccess(Match match) => new() { Match = match };
        public static implicit operator BanchoMatchJoinSuccess(ServersideMatch match) => new() { Match = match.MatchPacket };

    }

    public static partial class ClientOsuPackets {
        public static void MatchJoinSuccess(this ClientOsu clientOsu, BanchoMatchJoinSuccess matchUpdate) {
            Packet<BanchoMatchJoinSuccess> matchPacket = new() {
                PacketId   = PacketType.BanchoMatchUpdate,
                Compressed = false,
                PacketData = matchUpdate
            };

            clientOsu.SendData(matchPacket.ToBytes());
        }
    }
}
