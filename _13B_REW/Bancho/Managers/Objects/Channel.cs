using System.Collections.Generic;
using System.Net.Http;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Managers.Objects {
    public abstract class Channel {
        private readonly List<ClientOsu> _connectedClients = new();
        private          string          _name             = "";
        private readonly bool            _priviledgedRead;
        private readonly bool            _priviledgedWrite;

        public Channel(string name, bool priviledgedRead, bool priviledgedWrite) {
            this._name             = name;
            this._priviledgedRead  = priviledgedRead;
            this._priviledgedWrite = priviledgedWrite;
        }

        public string GetName() => this._name;

        public bool Join(ClientOsu clientOsu) {
            if (this._connectedClients.Contains(clientOsu)) {
                this._connectedClients.RemoveAll(client => client == clientOsu);
                this._connectedClients.Add(clientOsu);

                clientOsu.JoinChannel(this);
            }

            return true;
        }

        public bool TryJoin(ClientOsu clientOsu) {
            //TODO: privledges
            //if clientosu privledges lower than necessary then fuck off
            
            if (this._connectedClients.Contains(clientOsu)) {
                this._connectedClients.RemoveAll(client => client == clientOsu);
                this._connectedClients.Add(clientOsu);

                clientOsu.JoinChannel(this);
            }
            
            return true;
        }
    }

    public static partial class ClientOsuPackets {
        public static void JoinChannel(this ClientOsu clientOsu, Channel channel) {
            if (channel.Join(clientOsu)) {
                clientOsu.SendJoinSuccess(channel.GetName());
            }
        }
    }
}
