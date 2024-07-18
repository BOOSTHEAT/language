#nullable enable

using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public abstract class ChartWidget : Widget
{
  public FeedDecoration[] Content;
}

public sealed class PieChartWidget : ChartWidget
{
  public FeedDecoration[] Slices => Content;
}

public abstract class ChartYWidget : ChartWidget
{
  public Feed? YMin;
  public Feed? YMax;
}

public sealed class BarsWidget : ChartYWidget
{
  public FeedDecoration[] Bars => Content;
}


public abstract class ChartXTimeYWidget : ChartYWidget
{
  public ChartXTimeSpan XSpan;
}

public record ChartXTimeSpan(int Duration, TimeUnit TimeUnit)
{
  public int Duration { get; } = Duration;
  public TimeUnit TimeUnit { get; } = TimeUnit;
}

public sealed class StackedTimeBarsWidget : ChartXTimeYWidget
{
  public FeedDecoration[] TimeBars => Content;
}

public sealed class TimeLinesWidget : ChartXTimeYWidget
{
  public FeedDecoration[] TimeLines => Content;
}

public sealed class MultiChartWidget : Widget
{
  public ChartXTimeYWidget Left;
  public ChartXTimeYWidget Right;
}
