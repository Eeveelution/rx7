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

            foreach (Type channel in Assembly.GetAssembly(typeof(Channel))?.GetTypes()?.Where
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
        }
    }
}
