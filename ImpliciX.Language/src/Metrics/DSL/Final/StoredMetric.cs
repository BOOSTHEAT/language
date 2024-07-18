using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class StoredMetric : FluentStep, IMetricDefinition<MetricUrn>
{
  internal StoredMetric(IMetricBuilder builder) : base(builder)
  {
  }

  public MetricPeriod<StandardMetric> Group => new (
    Builder,
    (b, timeSpan, multiplier, timeUnit) => new StandardMetric(b.AddGroupPolicy(timeSpan, multiplier, timeUnit))
  );
}
