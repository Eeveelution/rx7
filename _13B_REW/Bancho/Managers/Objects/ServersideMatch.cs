using _13B_REW.Bancho.Packets.Enums;
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

        public bool Join(ClientOsu clientOsu, MatchJoin matchJoin) {
            lock (this._matchInformation.SlotStatuses) {
                for (int i = 0; i < 7; i++) {
                    if (this._matchInformation.SlotStatuses[i] == SlotStatus.Open) {

                    }
                }
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
                this.BroadcastPacket(new BanchoMatchUpdate(this._matchInformation));
                try {
                    Lobby.SenkoMultiplayerRooms[this._matchInformation.MatchId].HandleUpdate(this._matchInformation);
                    Lobby.BroadcastMatchUpdates();
                }
                catch {
                    // ignored
                }
            }
        }
        /// <summary>
        /// Broadcasts Packet to everyone in the match
        /// </summary>
        /// <param name="outgoingPacket">Packet</param>
        private void BroadcastPacket(OutgoingPacket outgoingPacket) {
            lock (this._matchInformation)
                foreach (ClientOsu clientOsu in this._matchInformation.ConnectedClients)
                    clientOsu?.QueuePacket(outgoingPacket);
        }
        /// <summary>
        /// Broadcasts Packet to everyone in the match Except `ClientOsu`
        /// </summary>
        /// <param name="clientOsu">Who not to send to</param>
        /// <param name="outgoingPacket">Packet</param>
        public void BroadcastPacketToOthers(ClientOsu clientOsu, OutgoingPacket outgoingPacket) {
            lock (this._matchInformation)
                foreach (ClientOsu client in this._matchInformation.ConnectedClients)
                    if(client != clientOsu)
                        client?.QueuePacket(outgoingPacket);
        }
        /// <summary>
        /// Gets the Slot ID of a Player in the Room
        /// </summary>
        /// <param name="userId">User ID to seek for</param>
        /// <returns>Slot ID</returns>
        private int GetSlotIdFromPlayerId(int userId) {
            for(int i = 0; i != 8; i++)
                lock (this._matchInformation)
                    if (this._matchInformation.SlotId[i] == userId)
                        return i;
            return -1;
        }
        #endregion
    }
}
