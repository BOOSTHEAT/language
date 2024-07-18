using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics
{
  public sealed class ScheduledDeviceMetric : FluentStep
  {
    internal ScheduledDeviceMetric(IMetricBuilder defBuilder) : base(defBuilder)
    {
    }

    public DeviceMonitoringMetric DeviceMonitoringOf(DeviceNode deviceNode)
    {
      if (deviceNode == null) throw new ArgumentNullException(nameof(deviceNode));

      return new DeviceMonitoringMetric(
        Builder
          .WithMetricKind(MetricKind.Communication)
          .WithInputUrn(deviceNode.Urn)
      );
    }
  }
}