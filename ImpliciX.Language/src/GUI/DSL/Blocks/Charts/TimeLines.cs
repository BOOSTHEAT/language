#nullable enable

using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed class TimeLines : XTimeChart<TimeLines>, ILeftYAxisChart, IRightYAxisChart
{
  public TimeLines(DecoratedUrn[] lines) : base(lines)
  {
  }

  public override Widget CreateWidget() => CreateXTimeWidget();
  
  public ChartXTimeYWidget CreateXTimeWidget() =>
    new TimeLinesWidget
    {
      Content = Data.Select(b => b.ToFeedDecorationAsTimeSeries()).ToArray(),
      Width = WidthValue,
      Height = HeightValue,
      YMin = YMinFeed,
      YMax = YMaxFeed,
      XSpan = new ChartXTimeSpan(TimeMultiplier, TimeUnit)
    };

  protected override TimeLines This => this;
}