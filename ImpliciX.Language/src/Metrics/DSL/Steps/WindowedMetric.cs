using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class WindowedMetric : FluentStep
{
    private readonly MetricComputation<StandardMetric> _metricComputation;

    internal WindowedMetric(IMetricBuilder builder) : base(builder)
    {
        _metricComputation = new MetricComputation<StandardMetric>(
            builder,
            (b, kind, urn) => new StandardMetric(b.WithMetricKind(kind).WithInputUrn(urn))
        );
    }

    public StandardMetric AccumulatorOf(Urn inputUrn) => _metricComputation.AccumulatorOf(inputUrn);
    public StandardMetric VariationOf(Urn inputUrn) => _metricComputation.VariationOf(inputUrn);
    public ScheduledStateMetric StateMonitoringOf<T>(PropertyUrn<T> inputUrn) => new (Builder.WithInputUrn(inputUrn, typeof(T)));
}