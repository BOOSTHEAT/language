using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class DeviceMonitoringMetric : FluentStep, IMetricDefinition<AnalyticsCommunicationCountersNode>
{
  internal DeviceMonitoringMetric(IMetricBuilder builder) : base(builder)
  {
  }
}