using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class Metrics
{
  public static NamedDeviceMetric Metric(AnalyticsCommunicationCountersNode node) => new(node);
  public static NamedMetric Metric(MetricUrn node) => new(node);
}