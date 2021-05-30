using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Chat.Channels {
    public class BasicChannel : Channel {

        public override string _name { get; set; } = "#basic";
        public override bool _priviledgedRead { get; set; } = false;
        public override bool _priviledgedWrite { get; set; } = false;
        public override List<ChannelRule> _channelRules { get; set; } = new();
    }
}
