using ImpliciX.Language.Model;
namespace ImpliciX.Language.GUI
{
  public class Measure<T> : Block
  {
    internal MeasureNode<T> Node { get; set; }
    public override Widget CreateWidget()
    {
      return new Text
      {
        Value = MeasureFeed.Subscribe(Node),
        Style = Font?.CreateStyle(),
        Width = WidthValue
      };
    }
  }
}