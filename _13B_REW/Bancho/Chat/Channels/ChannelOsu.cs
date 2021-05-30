using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Chat.Channels {
    public class ChannelOsu : Channel {
        public override string ChannelName { get; set; } = "#osu";
        public override bool PriviledgedRead { get; set; } = false;
        public override bool PriviledgedWrite { get; set; } = false;
        protected override List<ChannelRule> ChannelRules { get; set; } = new();
    }
}
