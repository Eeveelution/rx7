using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Chat.Channels {
    public class ChannelAnnounce : Channel {

        public override string ChannelName { get; set; } = "#announce";
        public override bool PriviledgedRead { get; set; } = false;
        public override bool PriviledgedWrite { get; set; } = true;
        protected override List<ChannelRule> ChannelRules { get; set; } = new();
    }
}
