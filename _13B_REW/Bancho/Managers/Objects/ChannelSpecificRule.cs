using System;

namespace _13B_REW.Bancho.Managers.Objects {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ChannelSpecificRule : Attribute {
        public string ChannelName { get; }

        public ChannelSpecificRule(string channelName) {
            this.ChannelName = channelName;
        }

        public ChannelSpecificRule(Channel channel) {
            this.ChannelName = channel.GetName();
        }
    }
}
