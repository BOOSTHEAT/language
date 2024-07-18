using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed record ChartsFacade
{
  public PieChart Pie(DecoratedUrn slice1, DecoratedUrn slice2, params DecoratedUrn[] slices) // slice1 and slice2 to force to have at least 2 slices per PieChart
    => new (new[] {slice1, slice2}.Concat(slices).ToArray());

  public Bars Bars(DecoratedUrn bar1, params DecoratedUrn[] bars) => new (bars.Prepend(bar1).ToArray());

  public StackedTimeBars StackedTimeBars(DecoratedUrn bar1, params DecoratedUrn[] bars) => new (bars.Prepend(bar1).ToArray());

  public TimeLines TimeLines(DecoratedUrn line1, params DecoratedUrn[] lines) => new (lines.Prepend(line1).ToArray());
  
  public MultiChart Multi(ILeftYAxisChart left, IRightYAxisChart right) => new (left, right);
}