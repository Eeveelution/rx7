using System.Collections.Generic;
using _13B_REW.Bancho.Managers.Objects;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Chat.ChannelRules {
    [ChannelSpecificRule("#osu")]
    public class BadWordFilter : ChannelRule {
        //TODO: replace this with a dictionary stored on disk to prevent filling this repo with bad words
        private readonly Dictionary<string, string> _badwords = new() {
            { "cunt", "nice person" }
        };

        public override bool RunRule(Channel channel, Message message) {
            foreach ((string word, string replacement) in this._badwords) {
                if (message.Text.Contains(word))
                    message.Text = message.Text.Replace(word, replacement);
            }

            return true;
        }
    }
}
