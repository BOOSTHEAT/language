using System;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class NamedSubMetric : FluentStep
{
  public MetricComputationForMetricIncluded As { get; }
  
  internal NamedSubMetric(IMetricBuilder builder, string subMetricName) : base(builder)
  {
    var name = subMetricName ?? throw new ArgumentNullException(nameof(subMetricName));
    As = new MetricComputationForMetricIncluded(
      builder,
      (b, metricKind, inputUrn) =>
        new ScheduledStateMetric(b.AddSubMetric(name, metricKind, inputUrn))
      );
  }
}