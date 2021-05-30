using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _13B_REW.Bancho.Chat.Channels;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Managers {
    public static class ChannelManager {
        private static ConcurrentDictionary<string, Channel> _channels = new();

        public static void InitializeChannels() {
            Dictionary<string, Channel> channels = new();

            //Get Every Channel Defined
            foreach (Type channel in Assembly.GetAssembly(typeof(Channel))?.GetTypes()?.Where
                //Where Every Class is Inherited by Channel
                (type =>
                     type.IsClass                         &&
                     type.IsSubclassOf(typeof(Channel)) &&
                     type != typeof(BasicChannel))) {
                try {
                    Channel foundChannel = (Channel) Activator.CreateInstance(channel);
                    channels.Add(foundChannel.GetName(), foundChannel);
                }
                catch (NullReferenceException) {
                    /**/
                }
                catch (ArgumentException) {
                    Console.WriteLine("[ALERT] 2 Channels of the Same name are defined...");
                }
            }

            //Gets all Channel rules
            foreach (Type rule in Assembly.GetAssembly(typeof(Channel))?.GetTypes()?.Where
                //Where Every Class is Inherited by Channel
                (type =>
                     type.IsClass                       &&
                     type.IsSubclassOf(typeof(ChannelRule))))
            {
                ChannelRule channelRule = (ChannelRule)Activator.CreateInstance(rule);

                List<Attribute> attributes = rule.GetCustomAttributes(typeof(ChannelSpecificRule)).ToList();

                if (attributes.Count != 0) {
                    ChannelSpecificRule channelSpecificRule = attributes[0] as ChannelSpecificRule;

                    if (channels.ContainsKey(channelSpecificRule?.ChannelName ?? string.Empty)) {
                        channels[channelSpecificRule?.ChannelName ?? string.Empty].AddRule(channelRule);
                    }

                    continue;
                }

                foreach (Channel channel in channels.Values) {
                    channel.AddRule(channelRule);
                }
            }
        }
    }
}
