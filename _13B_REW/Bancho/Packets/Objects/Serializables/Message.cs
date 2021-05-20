using _13B_REW.Bancho.Attributes;

namespace _13B_REW.Bancho.Packets.Objects.Serializables {
    public class Message : Serializable {
        [RetainDeclarationOrder] public string Sender { get; set; }
        [RetainDeclarationOrder] public string Text { get; set; }
        [RetainDeclarationOrder] public string Target { get; set; }
    }
}
