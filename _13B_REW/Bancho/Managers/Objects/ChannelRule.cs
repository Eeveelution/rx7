using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Managers.Objects {
    public abstract class ChannelRule {
        /// <summary>
        /// Runs a Message Through a Rule. For example Filtering out Bad words or Special Channel Only Commands
        /// </summary>
        /// <returns>Allow Message to send?</returns>
        public abstract bool RunRule(Channel channel, Message message);
    }
}
