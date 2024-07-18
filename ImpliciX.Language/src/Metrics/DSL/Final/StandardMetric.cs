using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class StandardMetric : FluentStep, IMetricDefinition<MetricUrn>
{
  internal StandardMetric(IMetricBuilder builder) : base(builder)
  {
  }

  public MetricOver<StoredMetric> Over => new (Builder, b=> new StoredMetric(b));

  public MetricPeriod<StandardMetric> Group => new (
    Builder,
    (b, timeSpan, multiplier, timeUnit) => new StandardMetric(b.AddGroupPolicy(timeSpan, multiplier, timeUnit))
    );
}
