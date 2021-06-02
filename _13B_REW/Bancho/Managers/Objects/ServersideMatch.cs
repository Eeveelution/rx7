using System;
using System.Collections.Generic;
using System.Linq;
using _13B_REW.Bancho.Packets.Chat;
using _13B_REW.Bancho.Packets.Enums;
using _13B_REW.Bancho.Packets.Multiplayer;
using _13B_REW.Bancho.Packets.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

// ReSharper disable InconsistentlySynchronizedField

namespace _13B_REW.Bancho.Managers.Objects {
    public class ServersideMatch {
        private Match _matchInformation;

        public ServersideMatch(ClientOsu clientOsu, Match match) {
            this._matchInformation = match;

            this._matchInformation.SlotIds[0]          = clientOsu.UserId;
            this._matchInformation.SlotStatuses[0]     = SlotStatus.NotReady;
            this._matchInformation.ConnectedClients[0] = clientOsu;
            this._matchInformation.HostClient          = clientOsu;
        }

        public Match MatchPacket {
            get {
                lock (this._matchInformation.SlotStatuses)
                    return this._matchInformation;
            }
        }

        public void Join(ClientOsu clientOsu, MatchJoin matchJoin) {
            lock (this._matchInformation.SlotStatuses) {
                for (int i = 0; i < 7; i++) {
                    if (this._matchInformation.SlotStatuses[i] == SlotStatus.Open) {
                        this.SetSlot(i, clientOsu);

                        clientOsu.ChannelAvailableAutojoin("#multiplayer");
                        clientOsu.MatchJoinSuccess(this);
                        clientOsu.CurrentMultiplayerMatch = this;

                        this.UpdateMatch();

                        return;
                    }
                }
                clientOsu.MatchJoinFail();
            }
        }

        public void Leave(ClientOsu clientOsu) {
            lock (this._matchInformation) {
                int slot = this.GetSlotIdFromPlayerId(clientOsu.UserId);

                if (slot == -1)
                    return;

                this.SetSlot(slot, null);

                if (clientOsu == this._matchInformation.HostClient)
                    this.HandleHostLeave(slot);

                clientOsu.ChannelRevoked("#multiplayer");

                if (this._matchInformation.UsedUpSlots == 0) {
                    this.Disband();

                    return;
                }

                this.UpdateMatch();
            }
        }

        public void TryChangeSlot(ClientOsu clientOsu, int slotId) {
            lock (this._matchInformation) {
                if (this._matchInformation.SlotStatuses[slotId] == SlotStatus.Locked)
                    return;
                if ((this._matchInformation.SlotStatuses[slotId] & SlotStatus.CompHasPlayer) > 0)
                    return;

                this.MoveSlot(this.GetSlotIdFromPlayerId(clientOsu.UserId), slotId);

                this.UpdateMatch();
            }
        }

        public void ChangeTeam(ClientOsu clientOsu) {
            lock (this._matchInformation) {
                int slot = this.GetSlotIdFromPlayerId(clientOsu.UserId);

                this._matchInformation.SlotTeams[slot] = this._matchInformation.SlotTeams[slot] switch {
                    SlotTeams.Blue    => SlotTeams.Red,
                    SlotTeams.Red     => SlotTeams.Blue,
                    SlotTeams.Neutral => SlotTeams.Red,
                    _                 => SlotTeams.Red
                };

                this.UpdateMatch();
            }
        }

        public void TransferHost(ClientOsu clientOsu, int slot) {
            lock (this._matchInformation) {
                this._matchInformation.HostUserId = this._matchInformation.SlotIds[slot];
                this._matchInformation.HostClient = this._matchInformation.ConnectedClients[slot];

                this._matchInformation.HostClient.TransferMatchHost();

                this.UpdateMatch();
            }
        }

        public void ReadyUp(ClientOsu clientOsu) {
            int slot = this.GetSlotIdFromPlayerId(clientOsu.UserId);

            if (slot == -1)
                return;

            lock (this._matchInformation)
                this._matchInformation.SlotStatuses[slot] = SlotStatus.Ready;

            this.UpdateMatch();
        }

        public void UnReady(ClientOsu clientOsu) {
            int slot = this.GetSlotIdFromPlayerId(clientOsu.UserId);

            if (slot == -1)
                return;

            lock (this._matchInformation)
                this._matchInformation.SlotStatuses[slot] = SlotStatus.NotReady;

            this.UpdateMatch();
        }

        public void StartGame(ClientOsu clientOsu) {
            lock (this._matchInformation) {
                this._matchInformation.InProgress = true;

                for(int i = 0; i != 8; i++)
                    if (this._matchInformation.ConnectedClients[i] != null)
                        this._matchInformation.SlotStatuses[i] = SlotStatus.Playing;

                this.UpdateMatch();
                this.BroadcastPacket(client => client.MultiMatchStart());
            }
        }

        public void LockSlot(ClientOsu clientOsu, int slotId) {
            lock (this._matchInformation) {
                if (clientOsu != this._matchInformation.HostClient || this._matchInformation.ConnectedClients[slotId] == this._matchInformation.HostClient)
                    return;

                if ((this._matchInformation.SlotStatuses[slotId] & SlotStatus.CompHasPlayer) > 0) {
                    ClientOsu droppedPlayer = this._matchInformation.ConnectedClients[slotId];
                    this.Leave(droppedPlayer);
                }

                if (this._matchInformation.SlotStatuses[slotId] == SlotStatus.Locked) {
                    this._matchInformation.SlotStatuses[slotId] = SlotStatus.Open;

                    this.UpdateMatch();

                    return;
                }

                if (this._matchInformation.SlotOpenCount > 2 && this._matchInformation.SlotStatuses[slotId] == SlotStatus.Open)
                    this._matchInformation.SlotStatuses[slotId] = SlotStatus.Locked;
            }

            this.UpdateMatch();
        }

        public void Disband() {
            lock (this._matchInformation) {
                MultiplayerManager.MatchesById.Remove(this._matchInformation.MatchId, out _);

                Bancho.BroadcastPacket(client => client.MatchDisband(this));
            }
        }

        public void HandleHostLeave(int hostSlot) {
            lock (this._matchInformation) {
                if (this._matchInformation.UsedUpSlots == 0) {
                    this.Disband();
                    return;
                }

                for (int i = 0; i != 8; i++) {
                    if (this._matchInformation.ConnectedClients[i] != null) {
                        this._matchInformation.HostClient = this._matchInformation.ConnectedClients[i];
                        this._matchInformation.HostUserId = this._matchInformation.ConnectedClients[i].UserId;

                        if(!this._matchInformation.InProgress)
                            this.MoveSlot(i, hostSlot);

                        this._matchInformation.HostClient.TransferMatchHost();
                    }
                }

                this.UpdateMatch();
            }

        }

        #region Helper Functions

        /// <summary>
        /// Sets a Slot to a Player
        /// </summary>
        /// <param name="slot">Where to place them</param>
        /// <param name="clientOsu">Who</param>
        private void SetSlot(int slot, ClientOsu clientOsu) {
            if (clientOsu == null) {
                this._matchInformation.SlotIds[slot] = -1;
                if (this._matchInformation.SlotStatuses[slot] != SlotStatus.Locked)
                    this._matchInformation.SlotStatuses[slot] = SlotStatus.Open;
                this._matchInformation.ConnectedClients[slot] = null;
            } else {
                this._matchInformation.SlotIds[slot]          = clientOsu.UserId;
                this._matchInformation.SlotStatuses[slot]     = SlotStatus.NotReady;
                this._matchInformation.ConnectedClients[slot] = clientOsu;
            }
        }
        /// <summary>
        /// Moves a player from a Slot to Another Slot
        /// </summary>
        /// <param name="oldSlot">From</param>
        /// <param name="newSlot">To</param>
        private void MoveSlot(int oldSlot, int newSlot) {
            lock (this._matchInformation) {
                if (oldSlot == newSlot) return;
                if (oldSlot == -1 || newSlot == -1) return;

                SlotStatus currentStatus = this._matchInformation.SlotStatuses[oldSlot];

                this.SetSlot(newSlot, this._matchInformation.ConnectedClients[oldSlot]);
                this.SetSlot(oldSlot, null);

                this._matchInformation.SlotStatuses[newSlot] = currentStatus;
            }
        }
        /// <summary>
        /// Broadcasts Match Updates
        /// </summary>
        private void UpdateMatch() {
            lock (this._matchInformation) {
                this.BroadcastPacket(client => client.MatchUpdate(this));
                try {
                    MultiplayerManager.MatchesById[this._matchInformation.MatchId].HandleUpdate(this._matchInformation);
                    MultiplayerManager.BroadcastMatchUpdates();
                }
                catch {
                    // ignored
                }
            }
        }
        /// <summary>
        /// Broadcasts Packet to everyone in the match
        /// </summary>
        /// <param name="packetAction">Packet Function</param>
        private void BroadcastPacket(Action<ClientOsu> packetAction) {
            lock (this._matchInformation)
                foreach (ClientOsu clientOsu in this._matchInformation.ConnectedClients.Where(client => client != null))
                    packetAction(clientOsu);
        }
        /// <summary>
        /// Broadcasts Packet to everyone in the match Except `ClientOsu`
        /// </summary>
        /// <param name="clientOsu">Who not to send to</param>
        /// <param name="packetAction">Packet Function</param>
        public void BroadcastPacketToOthers(ClientOsu clientOsu, Action<ClientOsu> packetAction) {
            lock (this._matchInformation)
                foreach (ClientOsu client in this._matchInformation.ConnectedClients)
                    if (client != clientOsu && clientOsu != null)
                        packetAction(clientOsu);
        }
        /// <summary>
        /// Gets the Slot ID of a Player in the Room
        /// </summary>
        /// <param name="userId">User ID to seek for</param>
        /// <returns>Slot ID</returns>
        private int GetSlotIdFromPlayerId(int userId) {
            for (int i = 0; i != 8; i++)
                lock (this._matchInformation)
                    if (this._matchInformation.SlotIds[i] == userId)
                        return i;
            return -1;
        }
        /// <summary>
        /// Handles a Match Update
        /// </summary>
        /// <param name="match">Read Match</param>
        private void HandleUpdate(Match match) {
            lock (this._matchInformation) {
                //Update all Member Variables
                this._matchInformation.MatchId          = (byte) match.MatchId;
                this._matchInformation.InProgress       = match.InProgress;
                this._matchInformation.MatchTeamType    = match.MatchTeamType;
                this._matchInformation.EnabledMods      = match.EnabledMods;
                this._matchInformation.GameName         = match.GameName;
                this._matchInformation.BeatmapText      = match.BeatmapText;
                this._matchInformation.BeatmapId        = match.BeatmapId;
                this._matchInformation.BeatmapChecksum  = match.BeatmapChecksum;
                this._matchInformation.SlotStatuses     = match.SlotStatuses;
                this._matchInformation.Password         = match.Password;
                this._matchInformation.MatchTeamType    = match.MatchTeamType;
                this._matchInformation.GamePlaymode     = match.GamePlaymode;
                this._matchInformation.MatchScoringType = match.MatchScoringType;
                this._matchInformation.SlotTeams        = match.SlotTeams;
            }
        }

        #endregion

    }
}
