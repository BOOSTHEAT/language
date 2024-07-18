using System;

namespace ImpliciX.Language.Model
{
    public class CommandNode<T> : ModelNode
    {
        public static CommandNode<T> Create(string urnToken, ModelNode parent)
        {
            return new CommandNode<T>(urnToken, parent);
        }

        private CommandNode(string urnToken, ModelNode parent) : base(urnToken, parent)
        {
            command = CommandUrn<T>.Build(Urn);
            measure = PropertyUrn<T>.Build(Urn,nameof(measure));
            status = PropertyUrn<MeasureStatus>.Build(Urn, nameof(status));
        }

        public CommandUrn<T> command { get; }
        public PropertyUrn<T> measure { get; }

        public PropertyUrn<MeasureStatus> status { get;}
        
        public static implicit operator CommandUrn<T>(CommandNode<T> node) => node.command;
        
        public static implicit operator String(CommandNode<T> node) => node.command;
    }
}