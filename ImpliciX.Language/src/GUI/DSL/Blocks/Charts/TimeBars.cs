#nullable enable

using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public abstract class TimeBars : XTimeChart<TimeBars>, ILeftYAxisChart
{
  protected TimeBars(DecoratedUrn[] bars) : base(bars)
  {
  }

  public override Widget CreateWidget() => CreateXTimeWidget();

  public ChartXTimeYWidget CreateXTimeWidget() =>
    new StackedTimeBarsWidget
    {
      Content = Data.Select(b => b.ToFeedDecorationAsTimeSeries()).ToArray(),
      Width = WidthValue,
      Height = HeightValue,
      YMin = YMinFeed,
      YMax = YMaxFeed,
      XSpan = new ChartXTimeSpan(TimeMultiplier, TimeUnit)
    };
  
  protected override TimeBars This => this;
}

public sealed class StackedTimeBars : TimeBars
{
  public StackedTimeBars(params DecoratedUrn[] bars) : base(bars)
  {
  }
}