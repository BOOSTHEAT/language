using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Metrics.Internals
{
  public readonly struct SubMetricDef
  {
    public string SubMetricName { get; }
    public MetricKind MetricKind { get; }
    public Urn InputUrn { get; }

    public SubMetricDef(string subMetricName, MetricKind metricKind, Urn inputUrn)
    {
      SubMetricName = subMetricName ?? throw new ArgumentNullException(nameof(subMetricName));
      MetricKind = metricKind;
      InputUrn = inputUrn ?? throw new ArgumentNullException(nameof(inputUrn));
    }
  }
}