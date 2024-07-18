using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class StoredStateMetric : FluentStep, IMetricDefinition<MetricUrn>
{
  internal StoredStateMetric(IMetricBuilder builder) : base(builder)
  {
  }

  public MetricPeriod<GroupedStateMetric> Group => new (
    Builder,
    (b, timeSpan, multiplier, timeUnit) => new GroupedStateMetric(b.AddGroupPolicy(timeSpan, multiplier, timeUnit))
  );
}
