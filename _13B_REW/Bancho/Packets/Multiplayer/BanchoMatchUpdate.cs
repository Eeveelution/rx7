using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Packets.Multiplayer {
    public class BanchoMatchUpdate : Serializable {
        public BanchoMatchUpdate() {}
        public BanchoMatchUpdate(Match match) => this.Match = match;
        public BanchoMatchUpdate(Stream readStream) => this.ReadFromStream(readStream);

        [RetainDeclarationOrder]
        public Match Match { get; set; }

        public static implicit operator Match(BanchoMatchUpdate matchUpdate) => matchUpdate.Match;
        public static implicit operator BanchoMatchUpdate(Match match) => new() { Match = match };
        public static implicit operator BanchoMatchUpdate(ServersideMatch match) => new() { Match = match.MatchPacket };
    }

    public static partial class ClientOsuPackets {
        public static void MatchUpdate(this ClientOsu clientOsu, BanchoMatchUpdate matchUpdate) {
            Packet<BanchoMatchUpdate> matchPacket = new() {
                PacketId   = PacketType.BanchoMatchUpdate,
                Compressed = false,
                PacketData = matchUpdate
            };

            clientOsu.SendData(matchPacket.ToBytes());
        }
    }
}
