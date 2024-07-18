using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus
{
    public class microcontroller : PrivateModelNode
    {
        private bootloader _bootloaderNode;
        
        public microcontroller(ModelNode parent) : base(nameof(microcontroller), parent)
        {
            bootloader = _bootloaderNode ??= new bootloader(this);
            board_state = new MeasureNode<BoardState>(nameof(board_state), this);
            active_partition = new MeasureNode<Partition>(nameof(active_partition),this);
            crc_partition_one = new MeasureNode<CRCStatus>(nameof(crc_partition_one), this);
            crc_partition_two = new MeasureNode<CRCStatus>(nameof(crc_partition_two), this);
            update_partition = PropertyUrn<Partition>.Build(Urn,nameof(update_partition));
            _set_active_partition = CommandNode<Partition>.Create("SET_ACTIVE_PARTITION",this);
            _reset = CommandNode<ResetArg>.Create("RESET", this);
        }
        public bootloader bootloader { get; }

        public MeasureNode<BoardState> board_state { get; }

        public MeasureNode<Partition> active_partition { get; }
        
        public MeasureNode<CRCStatus> crc_partition_one { get; }
        public MeasureNode<CRCStatus> crc_partition_two { get; }

        public PropertyUrn<Partition> update_partition { get; }

        public CommandNode<Partition> _set_active_partition { get; }
        public CommandNode<ResetArg> _reset { get; } 
    }
}