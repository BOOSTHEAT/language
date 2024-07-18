using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class MetricComputationForMetricIncluded : FluentStep
{
    private readonly MetricComputation<ScheduledStateMetric> _metricComputation;

    public ScheduledStateMetric AccumulatorOf(Urn inputUrn) => _metricComputation.AccumulatorOf(inputUrn);
    public ScheduledStateMetric VariationOf(Urn inputUrn) => _metricComputation.VariationOf(inputUrn);

    internal MetricComputationForMetricIncluded(IMetricBuilder builder, Func<IMetricBuilder, MetricKind, Urn, ScheduledStateMetric> nextFactory) : base(builder)
    {
        _metricComputation = new MetricComputation<ScheduledStateMetric>(builder, nextFactory);
    }
}