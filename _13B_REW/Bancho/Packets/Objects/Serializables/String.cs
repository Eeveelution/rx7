using System.IO;
using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class String : Serializable {
        public String(){}
        public String(string text) => this.Text = text;
        public String(Stream readStream) => this.ReadFromStream(readStream);
        [RetainDeclarationOrder] public string Text { get; set; }

        public static implicit operator string(String text) => text.Text;
        public static implicit operator String(string text) => new() { Text = text };
    }
}
