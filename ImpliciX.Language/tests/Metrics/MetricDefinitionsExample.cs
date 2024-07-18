using System;
using System.Linq;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NUnit.Framework;
using MetricsDSL = ImpliciX.Language.Metrics.Metrics;

namespace ImpliciX.Language.Tests.Metrics
{
  public class MetricsDefinitionExamples : MetricsDSL
  {
    private static IMetricDefinition[] metrics = new IMetricDefinition[]
    {
      Metric(mmodel.metrics.temperature_history)
        .Is.Hourly
        .GaugeOf(mmodel.measures.the_temperature)
        .Over.ThePast(2).Minutes,

      Metric(mmodel.metrics.pressure_stats)
        .Is.Every(5).Minutes
        .AccumulatorOf(mmodel.measures.the_pressure),

      Metric(mmodel.metrics.electricity_consumption)
        .Is.Every(10).Seconds
        .VariationOf(mmodel.measures.the_electrical_index),

      Metric(mmodel.metrics.long_term_temperature_variations)
        .Is.Every(10).Seconds
        .VariationOf(mmodel.measures.the_temperature)
        .Over.ThePast(15).Minutes
        .Group.Every(5).Minutes.Over.ThePast(1).Hours
        .Group.Hourly.Over.ThePast(5).Days,

      Metric(mmodel.metrics.long_term_temperature_variations)
        .Is.Every(3).Milliseconds
        .VariationOf(mmodel.measures.the_temperature)
        .Over.ThePast(300).Milliseconds
        .Group.Every(30).Milliseconds.Over.ThePast(1).Hours,

      Metric(mmodel.metrics.state_monitoring)
        .Is.Hourly
        .StateMonitoringOf(mmodel.measures.some_state),

      Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is.Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state)
        .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
        .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature),

      Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is.Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state)
        .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
        .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
        .Over.ThePast(300).Minutes
        .Group.Daily,

      Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is.Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state)
        .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
        .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
        .Over.ThePast(300).Minutes
        .Group.Daily.Over.ThePast(5).Days,

      Metric(mmodel.metrics.device_communications)
        .Is.Minutely
        .DeviceMonitoringOf(mmodel.measures.the_device),
    };

    [Test]
    public void RootUrnsCanBeComputedFromMetricDefinitions()
    {
      Urn GetRootUrnOf(IMetricDefinition def)
      {
        if(def is IMetricDefinition<MetricUrn>)
          return def.Builder.Build<Metric<MetricUrn>>().TargetUrn;
        if(def is IMetricDefinition<AnalyticsCommunicationCountersNode>)
          return def.Builder.Build<Metric<AnalyticsCommunicationCountersNode>>().TargetUrn;
        throw new Exception();
      }
      Assert.That(metrics.Select(GetRootUrnOf), Is.EqualTo(new Urn[]
      {
        mmodel.metrics.temperature_history,
        mmodel.metrics.pressure_stats,
        mmodel.metrics.electricity_consumption,
        mmodel.metrics.long_term_temperature_variations,
        mmodel.metrics.long_term_temperature_variations,
        mmodel.metrics.state_monitoring,
        mmodel.metrics.state_monitoring_with_detailed_data,
        mmodel.metrics.state_monitoring_with_detailed_data,
        mmodel.metrics.state_monitoring_with_detailed_data,
        mmodel.metrics.device_communications.Urn,
      }));
    }
  }
}