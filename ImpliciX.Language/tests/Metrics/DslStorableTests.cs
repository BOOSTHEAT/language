using System;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Model.TimeUnit;
using MetricsDSL = ImpliciX.Language.Metrics.Metrics;
using TimeUnit = ImpliciX.Language.Model.TimeUnit;

namespace ImpliciX.Language.Tests.Metrics;

public class MetricStorableTests : MetricsDSL
{
    #region Store standard metrics

    [TestCaseSource(nameof(TestCases))]
    public void GivenGaugeVariationAccumulatorMetrics_When_StoragePolicyIsDefined_Then_TheSemanticModelIsBuilt_It_Contains_The_Defined_StoragePolicy(StandardMetric metric, int duration, TimeUnit timeUnit, Option<StoragePolicy> expected)
    {
        var storableMetric = metric.Over.ThePast(duration);
        var actual = timeUnit switch
        {
            Years => storableMetric.Years,
            Quarters => storableMetric.Quarters,
            Months => storableMetric.Months,
            Weeks => storableMetric.Weeks,
            Days => storableMetric.Days,
            Hours => storableMetric.Hours,
            Minutes => storableMetric.Minutes,
            Seconds => storableMetric.Seconds,
            Milliseconds => storableMetric.Milliseconds,
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
        };

        Check.That(actual.ToSemantic().StoragePolicy).IsEqualTo(expected);
    }

    private static readonly StandardMetric GaugeMetric = Metric(mmodel.instrumentation.electrical_index)
        .Is
        .Hourly
        .GaugeOf(instrumentation.electrical_index.measure);

    private static readonly StandardMetric VariationMetric = Metric(mmodel.instrumentation.electrical_index)
        .Is
        .Hourly
        .VariationOf(instrumentation.electrical_index.measure);
    
    private static readonly StandardMetric AccumulatorMetric = Metric(mmodel.instrumentation.electrical_index)
        .Is
        .Hourly
        .AccumulatorOf(mmodel.measures.the_temperature);

    public static object[] TestCases =
    {
        new object[]{GaugeMetric, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{GaugeMetric, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{GaugeMetric, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{GaugeMetric, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{GaugeMetric, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{GaugeMetric, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{GaugeMetric, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{GaugeMetric, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{GaugeMetric, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},

        new object[]{VariationMetric, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{VariationMetric, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{VariationMetric, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{VariationMetric, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{VariationMetric, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{VariationMetric, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{VariationMetric, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{VariationMetric, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{VariationMetric, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},
        
        new object[]{AccumulatorMetric, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{AccumulatorMetric, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{AccumulatorMetric, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{AccumulatorMetric, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{AccumulatorMetric, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{AccumulatorMetric, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{AccumulatorMetric, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{AccumulatorMetric, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{AccumulatorMetric, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},
     
    };

    #endregion
    
    #region Store state metrics

    [TestCaseSource(nameof(StateTestCases))]
    public void GivenStateMetrics_When_StoragePolicyIsDefined_Then_TheSemanticModelIsBuilt_It_Contains_The_Defined_StoragePolicy(ScheduledStateMetric metric, int duration, TimeUnit timeUnit, Option<StoragePolicy> expected)
    {
        var storableMetric = metric.Over.ThePast(duration);
        var actual = timeUnit switch
        {
            Years => storableMetric.Years,
            Quarters => storableMetric.Quarters,
            Months => storableMetric.Months,
            Weeks => storableMetric.Weeks,
            Days => storableMetric.Days,
            Hours => storableMetric.Hours,
            Minutes => storableMetric.Minutes,
            Seconds => storableMetric.Seconds,
            Milliseconds => storableMetric.Milliseconds,
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
        };

        Check.That(actual.ToSemantic().StoragePolicy).IsEqualTo(expected);
    }

    private static readonly ScheduledStateMetric StateMetric = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is
        .Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state);

    private static readonly ScheduledStateMetric StateMetricWithInclusion = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is
        .Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state)
        .Including("foo").As.VariationOf(instrumentation.electrical_index.measure);

    public static object[] StateTestCases =
    {
        new object[]{StateMetric, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{StateMetric, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{StateMetric, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{StateMetric, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{StateMetric, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{StateMetric, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{StateMetric, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{StateMetric, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{StateMetric, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},

        new object[]{StateMetricWithInclusion, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{StateMetricWithInclusion, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{StateMetricWithInclusion, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{StateMetricWithInclusion, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{StateMetricWithInclusion, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{StateMetricWithInclusion, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{StateMetricWithInclusion, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{StateMetricWithInclusion, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{StateMetricWithInclusion, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},
    };

    #endregion
    
    #region Store grouped state metrics

    [TestCaseSource(nameof(GroupedStateTestCases))]
    public void GivenMetricsWithOneGroup_When_StoragePolicyIsDefined_Then_TheSemanticModelIsBuilt_It_Contains_The_Defined_StoragePolicy(GroupedStateMetric metric, int duration, TimeUnit timeUnit, Option<StoragePolicy> expected)
    {
        var storableMetric = metric.Over.ThePast(duration);
        var actual = timeUnit switch
        {
            Years => storableMetric.Years,
            Quarters => storableMetric.Quarters,
            Months => storableMetric.Months,
            Weeks => storableMetric.Weeks,
            Days => storableMetric.Days,
            Hours => storableMetric.Hours,
            Minutes => storableMetric.Minutes,
            Seconds => storableMetric.Seconds,
            Milliseconds => storableMetric.Milliseconds,
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
        };

        Check.That(actual.ToSemantic().GroupPolicies.First().StoragePolicy).IsEqualTo(expected);
        Check.That(actual.ToSemantic().StoragePolicy).IsEqualTo(Option<StoragePolicy>.None());
    }

    private static readonly GroupedStateMetric GroupedStateMetric = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
        .Is
        .Hourly
        .StateMonitoringOf(mmodel.measures.some_other_state)
        .Group.Daily;

    public static object[] GroupedStateTestCases =
    {
        new object[]{GroupedStateMetric, 1, Milliseconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Milliseconds))},
        new object[]{GroupedStateMetric, 1, Seconds, Option<StoragePolicy>.Some(new StoragePolicy(1, Seconds))},
        new object[]{GroupedStateMetric, 1, Minutes, Option<StoragePolicy>.Some(new StoragePolicy(1, Minutes))},
        new object[]{GroupedStateMetric, 1, Hours, Option<StoragePolicy>.Some(new StoragePolicy(1, Hours))},
        new object[]{GroupedStateMetric, 1, Days, Option<StoragePolicy>.Some(new StoragePolicy(1, Days))},
        new object[]{GroupedStateMetric, 1, Weeks, Option<StoragePolicy>.Some(new StoragePolicy(1, Weeks))},
        new object[]{GroupedStateMetric, 1, Months, Option<StoragePolicy>.Some(new StoragePolicy(1, Months))},
        new object[]{GroupedStateMetric, 1, Quarters, Option<StoragePolicy>.Some(new StoragePolicy(1, Quarters))},
        new object[]{GroupedStateMetric, 1, Years, Option<StoragePolicy>.Some(new StoragePolicy(1, Years))},
    };

    #endregion

    [Test]
    public void GivenMetricsWithManyGroups_When_StoragePolicyIsDefined_Then_TheSemanticModelIsBuilt_EachGroup_Contains_The_Corresponding_StoragePolicy()
    {
        var storableMetric = 
            Metric(mmodel.metrics.state_monitoring_with_detailed_data).Is.Hourly
                .StateMonitoringOf(mmodel.measures.some_other_state).Over.ThePast(1).Weeks
                .Group.Every(1).Days.Over.ThePast(5).Years
                .Group.Every(2).Days
                .Group.Every(3).Days.Over.ThePast(10).Years
            ;

        var sut = storableMetric.ToSemantic();

        Check.That(sut.StoragePolicy).IsEqualTo(Option<StoragePolicy>.Some(new StoragePolicy(1, TimeUnit.Weeks)));
        var groupPolicies = sut.GroupPolicies.ToArray(); 
        Check.That(groupPolicies[0].StoragePolicy)
            .IsEqualTo(Option<StoragePolicy>.Some(new StoragePolicy(5, TimeUnit.Years)));

        Check.That(groupPolicies[1].StoragePolicy)
            .IsEqualTo(Option<StoragePolicy>.None());

        Check.That(groupPolicies[2].StoragePolicy)
            .IsEqualTo(Option<StoragePolicy>.Some(new StoragePolicy(10, TimeUnit.Years)));
    }
}