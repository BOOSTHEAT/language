using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics
{
  public sealed class MetricComputation<TNext> : FluentStep
  {
    private readonly Func<IMetricBuilder, MetricKind, Urn, TNext> _nextFactory;
    
    public TNext GaugeOf(Urn inputUrn) => _nextFactory(Builder, MetricKind.Gauge, inputUrn);
    public TNext AccumulatorOf(Urn inputUrn) => _nextFactory(Builder, MetricKind.SampleAccumulator, inputUrn);
    public TNext VariationOf(Urn inputUrn) => _nextFactory(Builder, MetricKind.Variation, inputUrn);

    internal MetricComputation(IMetricBuilder builder, Func<IMetricBuilder, MetricKind, Urn, TNext> nextFactory) : base(builder)
    {
      _nextFactory = nextFactory ?? throw new ArgumentNullException(nameof(nextFactory));
    }
  }
}