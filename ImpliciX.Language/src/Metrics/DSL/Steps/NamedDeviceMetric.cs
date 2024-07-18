using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class NamedDeviceMetric : FluentStep
{
  public MetricPeriod<ScheduledDeviceMetric> Is { get; }

  internal NamedDeviceMetric(AnalyticsCommunicationCountersNode target)
    : base(new MetricBuilder<AnalyticsCommunicationCountersNode>(target))
  {
    Is = new MetricPeriod<ScheduledDeviceMetric>(
      Builder,
      (b, timeSpan, _, _) => new ScheduledDeviceMetric(b.WithPublicationPeriod(timeSpan))
    );
  }
}