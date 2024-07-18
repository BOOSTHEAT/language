using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI;

public abstract class XTimeChart<TChart>
  : ChartBaseWithYAxis<TChart> where TChart : ChartBase<TChart>
{
  protected XTimeChart(DecoratedUrn[] data) : base(data)
  {
  }

  public Over<TChart> Over => new((multiplier, unit, span) =>
    Chain(() =>
    {
      TimeMultiplier = multiplier;
      TimeUnit = unit;
    }));

  public int TimeMultiplier { get; private set; }
  public TimeUnit TimeUnit { get; private set; }
}