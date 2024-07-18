using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Metrics.Metrics;
using static ImpliciX.Language.Model.TimeUnit;

namespace ImpliciX.Language.Tests.Metrics;

public class DslStateMonitoringTests
{
    [Test]
    public void WhenIStateMonitoringOf()
    {
        var metric = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
            .Is
            .Hourly
            .StateMonitoringOf(mmodel.measures.some_other_state);

        Check.That(metric).InheritsFrom<IMetricDefinition>();
        var sut = metric.ToSemantic();
        Check.That(sut).InheritsFrom<IMetric>();

        Check.That(sut.Kind).IsEqualTo(MetricKind.State);
        Check.That(sut.Target).IsEqualTo(mmodel.metrics.state_monitoring_with_detailed_data);
        Check.That(sut.InputUrn).IsEqualTo(mmodel.measures.some_other_state);
        Check.That(sut.InputType.GetValue()).IsEqualTo(typeof(TheState));
        Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));
    }

    [Test]
    public void GivenInclusions_WhenIStateMonitoringOf()
    {
        var metric = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
            .Is
            .Hourly
            .StateMonitoringOf(mmodel.measures.some_other_state)
            .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
            .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature);

        Check.That(metric).InheritsFrom<IMetricDefinition>();
        var sut = metric.ToSemantic();
        Check.That(sut).InheritsFrom<IMetric>();

        Check.That(sut.Kind).IsEqualTo(MetricKind.State);
        Check.That(sut.Target).IsEqualTo(mmodel.metrics.state_monitoring_with_detailed_data);
        Check.That(sut.InputUrn).IsEqualTo(mmodel.measures.some_other_state);
        Check.That(sut.InputType.GetValue()).IsEqualTo(typeof(TheState));
        Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));

        var subMetricDefs = sut.SubMetricDefs.ToArray();
        Check.That(subMetricDefs).HasSize(2);

        var subMetricVariationDef = subMetricDefs.First();
        Check.That(subMetricVariationDef.SubMetricName).IsEqualTo("electricity_consumption");
        Check.That(subMetricVariationDef.MetricKind).IsEqualTo(MetricKind.Variation);
        Check.That(subMetricVariationDef.InputUrn).IsEqualTo(mmodel.measures.the_electrical_index);

        var subMetricAccumulatorDef = subMetricDefs[1];
        Check.That(subMetricAccumulatorDef.SubMetricName).IsEqualTo("temperature_stats");
        Check.That(subMetricAccumulatorDef.MetricKind).IsEqualTo(MetricKind.SampleAccumulator);
        Check.That(subMetricAccumulatorDef.InputUrn).IsEqualTo(mmodel.measures.the_temperature);
    }

    #region Group multi-resolution on state metrics without inclusions

    private static object[] _stateMonitoringWithGroupsCases =
    {
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(8, Hours)})
        },
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Group.Every(30).Minutes
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(30, Minutes), (8, Hours)})
        },
    };

    [TestCaseSource(nameof(_stateMonitoringWithGroupsCases))]
    public void GivenGroupSyntaxWithStateMonitoringWithoutInclusions_WhenSemanticModelIsBuilt_ThenIGetGroupPoliciesExpected(
        Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>> sutFactory,
        IEnumerable<GroupPolicy> groupPoliciesExpected)
    {
        var metricBase = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
            .Is
            .Hourly
            .StateMonitoringOf(mmodel.measures.some_other_state);

        var sut = sutFactory(metricBase).ToSemantic();

        Check.That(sut.Kind).IsEqualTo(MetricKind.State);
        Check.That(sut.Target).IsEqualTo(mmodel.metrics.state_monitoring_with_detailed_data);
        Check.That(sut.InputUrn).IsEqualTo(mmodel.measures.some_other_state);
        Check.That(sut.InputType.GetValue()).IsEqualTo(typeof(TheState));
        Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));

        Check.That(sut.GroupPolicies).Contains(groupPoliciesExpected);
    }

    #endregion

    #region Group multi-resolution on state metrics with inclusions

    private static object[] _stateMonitoringWithInclusionsAndGroupsCases =
    {
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
                .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)),
            Enumerable.Empty<GroupPolicy>()
        },
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
                .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(8, Hours)})
        },
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
                .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
                .Group.Every(30).Minutes
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(30, Minutes), (8, Hours)})
        },
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
                .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
                .Group.Daily
                .Group.Every(30).Minutes
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(1, Days), (30, Minutes), (8, Hours)})
        },
        new object[]
        {
            new Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>>(metricBase => metricBase
                .Including("electricity_consumption").As.VariationOf(mmodel.measures.the_electrical_index)
                .Including("temperature_stats").As.AccumulatorOf(mmodel.measures.the_temperature)
                .Group.Daily
                .Group.Every(15).Minutes
                .Group.Every(7).Days
                .Group.Every(8).Hours),
            DslHelpers.CreateGroupPolicies(new[] {(1, Days), (15, Minutes), (7, Days), (8, Hours)})
        }
    };

    [TestCaseSource(nameof(_stateMonitoringWithInclusionsAndGroupsCases))]
    public void GivenUseGroupSyntaxWithStateMonitoringAndInclusions_WhenSemanticModelIsBuilt_ThenIGetGroupPoliciesExpected(
        Func<ScheduledStateMetric, IMetricDefinition<MetricUrn>> sutFactory,
        IEnumerable<GroupPolicy> groupPoliciesExpected)
    {
        var metricBase = Metric(mmodel.metrics.state_monitoring_with_detailed_data)
            .Is
            .Hourly
            .StateMonitoringOf(mmodel.measures.some_other_state);

        var sut = sutFactory(metricBase).ToSemantic();

        Check.That(sut.Kind).IsEqualTo(MetricKind.State);
        Check.That(sut.Target).IsEqualTo(mmodel.metrics.state_monitoring_with_detailed_data);
        Check.That(sut.InputUrn).IsEqualTo(mmodel.measures.some_other_state);
        Check.That(sut.InputType.GetValue()).IsEqualTo(typeof(TheState));
        Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));

        var subMetricDefs = sut.SubMetricDefs.ToArray();
        Check.That(subMetricDefs).HasSize(2);

        var subMetricVariationDef = subMetricDefs.First();
        Check.That(subMetricVariationDef.SubMetricName).IsEqualTo("electricity_consumption");
        Check.That(subMetricVariationDef.MetricKind).IsEqualTo(MetricKind.Variation);
        Check.That(subMetricVariationDef.InputUrn).IsEqualTo(mmodel.measures.the_electrical_index);

        var subMetricAccumulatorDef = subMetricDefs[1];
        Check.That(subMetricAccumulatorDef.SubMetricName).IsEqualTo("temperature_stats");
        Check.That(subMetricAccumulatorDef.MetricKind).IsEqualTo(MetricKind.SampleAccumulator);
        Check.That(subMetricAccumulatorDef.InputUrn).IsEqualTo(mmodel.measures.the_temperature);

        Check.That(sut.GroupPolicies).Contains(groupPoliciesExpected);
    }

    #endregion
}