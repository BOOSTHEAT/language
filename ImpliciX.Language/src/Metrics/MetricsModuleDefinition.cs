using System;
namespace ImpliciX.Language.Metrics
{
  public class MetricsModuleDefinition
  {
    public TimeSpan SnapshotInterval{ get; set; }
    public IMetricDefinition[] Metrics{ get; set; }
  }
}