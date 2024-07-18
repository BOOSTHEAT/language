using ImpliciX.Language.Model;
namespace ImpliciX.Language.Modbus
{
    public class firmware_update : PrivateModelNode
    {
        public firmware_update(ModelNode parent) : base(nameof(firmware_update), parent)
        {
            _send_first_frame = CommandNode<FirstFrame>.Create("SEND_FIRST_FRAME", this);
            _send_chunk = CommandNode<Chunk>.Create("SEND_CHUNK",this);
        }



        public CommandNode<FirstFrame> _send_first_frame { get; }
        public CommandNode<Chunk> _send_chunk { get; }
    }
}