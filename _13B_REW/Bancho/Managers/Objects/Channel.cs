using System.Collections.Generic;

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

        public bool Join(ClientOsu clientOsu) {
            //TODO: privledges
        }
    }
}
