// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public enum MetricKind : ushort
{
  Communication = 0,
  Gauge = 1,
  State = 2,
  SampleAccumulator = 3,
  Variation = 4
}