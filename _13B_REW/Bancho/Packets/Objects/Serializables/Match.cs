using System.IO;
using _13B_REW.Bancho.Packets.Enums;
using EeveeTools.Helpers;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class Match : Serializable {
        public ushort MatchId;
        public bool   InProgress;
        public Mods   EnabledMods;

        public string GameName;
        public string Password = null;

        public string BeatmapChecksum;
        public int    BeatmapId;
        public string BeatmapText;

        public SlotStatus[] SlotStatuses = new SlotStatus[8];
        public SlotTeams[]  SlotTeams    = new SlotTeams[8];
        public int[]        SlotIds      = new int[8];

        public int       HostUserId;
        public PlayModes GamePlaymode;

        public MatchScoringTypes MatchScoringType;
        public MatchTeamTypes    MatchTeamType;

        public Match() {}
        public Match(Stream readStream) => this.ReadFromStream(readStream);

        public override void ReadFromStream(Stream stream) {
            using BanchoReader reader = new(stream);

            reader.ReadByte();
        }
    }
}
