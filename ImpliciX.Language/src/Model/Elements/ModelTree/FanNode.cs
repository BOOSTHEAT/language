namespace ImpliciX.Language.Model
{

  public class FanNode : ModelNode
  {
    public FanNode(string urnToken, ModelNode parent) : base(urnToken, parent)
    {
      _throttle = CommandNode<Percentage>.Create("THROTTLE", this);
    }
    public CommandNode<Percentage> _throttle { get; }

  }
}