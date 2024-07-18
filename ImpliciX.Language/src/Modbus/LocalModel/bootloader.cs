using ImpliciX.Language.Model;
namespace ImpliciX.Language.Modbus
{
    public class bootloader : PrivateModelNode
    {
        public bootloader(ModelNode parent) : base(nameof(bootloader), parent)
        {
            _switch = CommandNode<MCUBootloader>.Create("SWITCH", this);
        }
        public CommandNode<MCUBootloader> _switch { get; }
    }
}