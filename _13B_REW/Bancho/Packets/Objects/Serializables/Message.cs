using System.IO;
using _13B_REW.Bancho.Attributes;
using _13B_REW.Bancho.Managers;
using _13B_REW.Bancho.Managers.Objects;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class Message : Serializable {
        public Message(){}
        public Message(string sender, string text, string target) {
            this.Sender = sender;
            this.Text   = text;
            this.Target = sender;
        }
        public Message(Stream readStream) => this.ReadFromStream(readStream);
        [RetainDeclarationOrder] public string Sender { get; set; }
        [RetainDeclarationOrder] public string Text { get; set; }
        [RetainDeclarationOrder] public string Target { get; set; }
    }

    public static class MessageExtensions {
        public static Channel GetChannel(this Message message) {
            try {
                return ChannelManager.Channels[message.Target];
            }
            catch {
                return null;
            }
        }
    }
}
