using System;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Model;

namespace ImpliciX.Language;

public class FrozenTimeSeriesDefinition
{
  public ITimeSeries[] TimeSeries { get; set; } = Array.Empty<ITimeSeries>();
  public IMetricDefinition[] Metrics { get; set; } = Array.Empty<IMetricDefinition>();
  
  [Obsolete("should use TimeSeries")]
  public Urn[] Urns { get; set; } = Array.Empty<Urn>();
}