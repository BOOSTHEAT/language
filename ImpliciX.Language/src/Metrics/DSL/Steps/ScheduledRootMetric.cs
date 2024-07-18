using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics
{
  public sealed class ScheduledRootMetric : FluentStep
  {
    private readonly MetricComputation<StandardMetric> _metricComputation;

    internal ScheduledRootMetric(IMetricBuilder builder) : base(builder)
    {
      _metricComputation = new MetricComputation<StandardMetric>(
        builder,
        (b, kind, urn) => new StandardMetric(b.WithMetricKind(kind).WithInputUrn(urn))
      );
    }

    public WindowableMetric OnAWindowOf(int timeUnitMultiplier)
      => new (Builder, builder => new WindowedMetric(builder), timeUnitMultiplier);
    
    public StandardMetric GaugeOf(Urn inputUrn) => _metricComputation.GaugeOf(inputUrn);
    public StandardMetric AccumulatorOf(Urn inputUrn) => _metricComputation.AccumulatorOf(inputUrn);
    public StandardMetric VariationOf(Urn inputUrn) => _metricComputation.VariationOf(inputUrn);
    public ScheduledStateMetric StateMonitoringOf<T>(PropertyUrn<T> inputUrn) => new(Builder.WithInputUrn(inputUrn, typeof(T)));
  }
}