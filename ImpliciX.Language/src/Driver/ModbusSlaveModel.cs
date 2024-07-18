using ImpliciX.Language.Model;

namespace ImpliciX.Language.Driver
{
  public class ModbusSlaveModel
  {
    public CommandNode<NoArg> Commit { get; set; }
    public CommandNode<NoArg> Rollback { get; set; }
  }
}