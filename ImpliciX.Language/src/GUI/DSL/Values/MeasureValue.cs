using ImpliciX.Language.Model;
namespace ImpliciX.Language.GUI
{
  public class MeasureValue<T> : Value
  {
    private readonly MeasureNode<T> _measure;
    public MeasureValue(MeasureNode<T> measure) => _measure = measure;
    public override Feed CreateFeed() => MeasureFeed.Subscribe(_measure);
  }
}