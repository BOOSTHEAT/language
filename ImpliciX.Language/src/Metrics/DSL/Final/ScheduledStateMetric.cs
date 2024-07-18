using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class ScheduledStateMetric : FluentStep, IMetricDefinition<MetricUrn>
{
  internal ScheduledStateMetric(IMetricBuilder metricBuilder)
    : base(metricBuilder.WithMetricKind(MetricKind.State))
  {
  }

  public NamedSubMetric Including(string subMetricName) => new(Builder, subMetricName);

  public MetricOver<StoredStateMetric> Over => new (Builder, b=> new StoredStateMetric(b));

  public MetricPeriod<GroupedStateMetric> Group => new (
    Builder,
    (b, timeSpan, multiplier, timeUnit) => new GroupedStateMetric(b.AddGroupPolicy(timeSpan, multiplier, timeUnit))
  );

}