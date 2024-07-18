using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus
{
    public class brahma : PrivateModelNode
    {
        public brahma(ModelNode parent) : base(nameof(brahma), parent)
        {
            _power = CommandNode<PowerSupply>.Create("POWER", this);
            _reset = CommandNode<NoArg>.Create("RESET", this);
            _start = CommandNode<NoArg>.Create("START", this);
            _stop = CommandNode<NoArg>.Create("STOP", this);
        }
        
        public CommandNode<PowerSupply> _power { get; }
        public CommandNode<NoArg> _reset { get; }
        public CommandNode<NoArg> _start { get; }
        public CommandNode<NoArg> _stop { get; }

    }
}