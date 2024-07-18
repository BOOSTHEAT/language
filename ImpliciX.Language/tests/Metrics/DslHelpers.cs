using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Tests.Metrics;

public static class DslHelpers
{
  public static StandardMetric CreateGaugeMetric(TimeUnit period = TimeUnit.Hours)
    => GetMetricWithPeriod(mmodel.instrumentation.electrical_index, period)
      .GaugeOf(instrumentation.electrical_index.measure);

  public static StandardMetric CreateVariationMetric(TimeUnit period = TimeUnit.Hours)
    => GetMetricWithPeriod(mmodel.instrumentation.electrical_index, period)
      .VariationOf(instrumentation.electrical_index.measure);

  public static StandardMetric CreateAccumulatorMetric(TimeUnit period = TimeUnit.Hours)
    => GetMetricWithPeriod(mmodel.instrumentation.electrical_index, period)
      .AccumulatorOf(instrumentation.electrical_index.measure);

  public static ScheduledStateMetric CreateStateMonitoringMetric(TimeUnit period = TimeUnit.Hours)
    => GetMetricWithPeriod(mmodel.metrics.state_monitoring_with_detailed_data, period)
      .StateMonitoringOf(mmodel.measures.some_other_state);
  
  public static IEnumerable<GroupPolicy> CreateGroupPolicies(IEnumerable<(int, TimeUnit)> multiplierTimeUnits)
    => multiplierTimeUnits.Select(o => new GroupPolicy(TupleToTimeSpan(o), o.Item1, o.Item2));

  private static ScheduledRootMetric GetMetricWithPeriod(MetricUrn metricUrn, TimeUnit period)
  {
    var metric = Language.Metrics.Metrics.Metric(metricUrn)
      .Is;

    return period switch
    {
      TimeUnit.Days => metric.Daily,
      TimeUnit.Hours => metric.Hourly,
      TimeUnit.Minutes => metric.Minutely,
      TimeUnit.Seconds => metric.Every(1).Seconds,
      TimeUnit.Milliseconds => metric.Every(1).Milliseconds,
      _ => throw new ArgumentOutOfRangeException(nameof(period), period, null)
    };
  }

  private static TimeSpan TupleToTimeSpan((int, TimeUnit) multiplierTimeUnit)
  {
    var (multiplier, timeUnit) = multiplierTimeUnit;
    return timeUnit switch
    {
      TimeUnit.Milliseconds => TimeSpan.FromMilliseconds(multiplier),
      TimeUnit.Seconds => TimeSpan.FromSeconds(multiplier),
      TimeUnit.Minutes => TimeSpan.FromMinutes(multiplier),
      TimeUnit.Hours => TimeSpan.FromHours(multiplier),
      TimeUnit.Days => TimeSpan.FromDays(multiplier),
      _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
    };
  }
}