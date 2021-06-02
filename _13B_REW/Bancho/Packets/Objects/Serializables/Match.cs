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

        public bool[]       SkipRequested    = new bool[8];
        public bool[]       PlayersLoaded    = new bool[8];
        public bool[]       PlayersCompleted = new bool[8];
        public ScoreFrame[] ScoreFrames      = new ScoreFrame[8];


        public ClientOsu[] ConnectedClients = new ClientOsu[8];

        public int       HostUserId;
        public ClientOsu HostClient;

        public PlayModes GamePlaymode;

        public MatchScoringTypes MatchScoringType;
        public MatchTeamTypes    MatchTeamType;

        public Match() {}
        public Match(Stream readStream) => this.ReadFromStream(readStream);

        public override void ReadFromStream(Stream stream) {
            using BanchoReader reader = new(stream);

            this.MatchId    = reader.ReadUInt16();
            this.InProgress = reader.ReadBoolean();

            reader.ReadByte();

            this.EnabledMods     = (Mods) reader.ReadInt16();
            this.GameName        = reader.ReadString();
            this.Password        = reader.ReadString();
            this.BeatmapChecksum = reader.ReadString();
            this.BeatmapId       = reader.ReadInt32();
            this.BeatmapText     = reader.ReadString();

            for (int i = 0; i != 8; i++)
                this.SlotStatuses[i] = (SlotStatus) reader.ReadByte();
            for (int i = 0; i != 8; i++)
                this.SlotTeams[i] = (SlotTeams) reader.ReadByte();
            for (int i = 0; i != 8; i++)
                this.SlotIds[i] = (((this.SlotStatuses[i] & SlotStatus.CompHasPlayer) > SlotStatus.None) ? reader.ReadInt32() : -1);

            this.HostUserId   = reader.ReadInt32();
            this.GamePlaymode = (PlayModes) reader.ReadByte();

            this.MatchScoringType = (MatchScoringTypes) reader.ReadByte();
            this.MatchTeamType    = (MatchTeamTypes) reader.ReadByte();
        }

        public override void WriteToStream(Stream stream) {
            using BanchoWriter writer = new(stream);

            writer.Write((short) this.MatchId);
            writer.Write(this.InProgress);
            writer.Write((byte) 0);
            writer.Write((short) this.EnabledMods);
            writer.Write(this.GameName);
            writer.Write(string.IsNullOrEmpty(this.Password));
            writer.Write(this.BeatmapChecksum);
            writer.Write(this.BeatmapId);
            writer.Write(this.BeatmapText);

            for (int i = 0; i != 8; i++)
                writer.Write((byte) this.SlotStatuses[i]);
            for (int i = 0; i != 8; i++)
                writer.Write((byte) this.SlotTeams[i]);
            for (int i = 0; i != 8; i++)
                if ((this.SlotStatuses[i] & SlotStatus.CompHasPlayer) > 0)
                    writer.Write(this.SlotIds[i]);

            writer.Write(this.HostUserId);
            writer.Write((byte) this.GamePlaymode);

            writer.Write((byte) this.MatchScoringType);
            writer.Write((byte) this.MatchTeamType);
        }

        #region Getters

        public int SlotOpenCount {
            get {
                int count = 0;

                for (int i = 0; i < 8; i++)
                    if (this.SlotStatuses[i] != SlotStatus.Locked)
                        count++;

                return count;
            }
        }

        /// <summary>
        /// Have all Players finished?
        /// </summary>
        public bool AllPlayersFinished {
            get {
                int playersFinished = 0;

                for (int i = 0; i < 8; i++)
                    if (this.PlayersCompleted[i] == true)
                        playersFinished++;

                return playersFinished == this.UsedUpSlots;
            }
        }

        /// <summary>
        /// Have all Players Skipped?
        /// </summary>
        public bool AllPlayersSkipped {
            get {
                for (int i = 0; i < 8; i++)
                    if (this.SlotStatuses[i] == SlotStatus.Playing && !this.SkipRequested[i])
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Have all Players Loaded?
        /// </summary>
        public bool AllPlayersLoaded {
            get {
                int playersLoaded = 0;

                for (int i = 0; i < 8; i++)
                    if (this.PlayersLoaded[i] == true)
                        playersLoaded++;

                return playersLoaded == this.UsedUpSlots;
            }
        }

        /// <summary>
        /// Gets Amount of slots that are still in use
        /// </summary>
        public int UsedUpSlots {
            get {
                int count = 0;

                for (int i = 0; i < 8; i++)
                    if ((this.SlotStatuses[i] & SlotStatus.CompHasPlayer) > 0)
                        count++;

                return count;
            }
        }

        #endregion

    }
}
