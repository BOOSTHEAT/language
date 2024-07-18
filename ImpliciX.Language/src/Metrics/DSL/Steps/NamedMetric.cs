using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics
{
  public sealed class NamedMetric : FluentStep
  {
    public MetricPeriod<ScheduledRootMetric> Is { get; }

    internal NamedMetric(MetricUrn target) : base(new MetricBuilder<MetricUrn>(target))
    {
      Is = new MetricPeriod<ScheduledRootMetric>(
        Builder,
        (b, timeSpan, _, _) => new ScheduledRootMetric(b.WithPublicationPeriod(timeSpan))
        );
    }
  }
}