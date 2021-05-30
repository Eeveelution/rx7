using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using _13B_REW.Bancho.Packets;
using _13B_REW.Bancho.Packets.Objects;

namespace _13B_REW.Bancho.Managers.Objects {
    public abstract class Channel {
        private readonly List<ClientOsu> _connectedClients = new();

        public abstract string ChannelName { get; set; }
        public abstract bool PriviledgedRead { get; set; }
        public abstract bool PriviledgedWrite { get; set; }

        protected abstract List<ChannelRule> ChannelRules { get; set; }

        private readonly object _channelRuleLock = new();

        public virtual void AddRule(ChannelRule rule) {
            this.ChannelRules ??= new List<ChannelRule>();

            lock (this._channelRuleLock)
                if (!this.ChannelRules.Contains(rule))
                    this.ChannelRules.Add(rule);

        }

        public virtual string GetName() => this.ChannelName;

        public virtual bool Join(ClientOsu clientOsu) {
            if (this._connectedClients.Contains(clientOsu)) {
                this._connectedClients.RemoveAll(client => client == clientOsu);
                this._connectedClients.Add(clientOsu);

                clientOsu.JoinChannel(this);
            }

            return true;
        }

        public virtual bool TryJoin(ClientOsu clientOsu) {
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
