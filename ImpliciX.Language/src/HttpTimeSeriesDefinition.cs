using System;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Model;

namespace ImpliciX.Language;

public class HttpTimeSeriesDefinition
{
  public TimeSeriesWithRetention[] TimeSeries { get; init; } = Array.Empty<TimeSeriesWithRetention>();
  public IMetricDefinition[] Metrics { get; init; } = Array.Empty<IMetricDefinition>();
}