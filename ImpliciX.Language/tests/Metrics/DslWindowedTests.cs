using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Metrics.Metrics;
using TimeUnit = ImpliciX.Language.Model.TimeUnit;

namespace ImpliciX.Language.Tests.Metrics;

public class DslWindowedTests
{
    private static IEnumerable<object[]> _metricWindowOfCases(Func<int, TimeUnit, IMetricDefinition> createMetric)
    {
        yield return new object[] {createMetric(1, TimeUnit.Milliseconds), new WindowPolicy(1, TimeUnit.Milliseconds)};
        yield return new object[] {createMetric(1, TimeUnit.Seconds), new WindowPolicy(1, TimeUnit.Seconds)};
        yield return new object[] {createMetric(2, TimeUnit.Minutes), new WindowPolicy(2, TimeUnit.Minutes)};
        yield return new object[] {createMetric(3, TimeUnit.Hours), new WindowPolicy(3, TimeUnit.Hours)};
        yield return new object[] {createMetric(4, TimeUnit.Days), new WindowPolicy(4, TimeUnit.Days)};
        yield return new object[] {createMetric(5, TimeUnit.Weeks), new WindowPolicy(5, TimeUnit.Weeks)};
        yield return new object[] {createMetric(6, TimeUnit.Months), new WindowPolicy(6, TimeUnit.Months)};
        yield return new object[] {createMetric(7, TimeUnit.Quarters), new WindowPolicy(7, TimeUnit.Quarters)};
        yield return new object[] {createMetric(8, TimeUnit.Years), new WindowPolicy(8, TimeUnit.Years)};
    }

    public static IEnumerable<object[]> MetricsWindowOfCases()
    {
        var metricFactories = new[]
        {
            new Func<int, TimeUnit, IMetricDefinition>((timeUnitMultiplier, timeUnit) =>
                CreateMetricWithWindowOf(mmodel.instrumentation.electrical_index, timeUnitMultiplier, timeUnit)
                    .VariationOf(instrumentation.electrical_index.measure)),
            new Func<int, TimeUnit, IMetricDefinition>((timeUnitMultiplier, timeUnit) =>
                CreateMetricWithWindowOf(mmodel.production.main_circuit.supply_temperature, timeUnitMultiplier, timeUnit)
                    .AccumulatorOf(production.main_circuit.supply_temperature.measure)),
            new Func<int, TimeUnit, IMetricDefinition>((timeUnitMultiplier, timeUnit) =>
                CreateMetricWithWindowOf(mmodel.metrics.state_monitoring_with_detailed_data, timeUnitMultiplier, timeUnit)
                    .StateMonitoringOf(mmodel.measures.some_other_state))
        };

        return metricFactories.SelectMany(_metricWindowOfCases);
    }

    [TestCaseSource(nameof(MetricsWindowOfCases))]
    public void WhenIUseOnAWindowOf_ThenMetricWindowPeriodHasValueAsExpected(IMetricDefinition<MetricUrn> metric, WindowPolicy windowPolicyExpected)
    {
        var sut = metric.ToSemantic();
        Check.That(sut.WindowPolicy.IsSome).IsTrue();
        Check.That(sut.WindowPolicy.GetValue()).IsEqualTo(windowPolicyExpected);
    }

    private static IEnumerable<object[]> _withoutWindowOfCases()
    {
        yield return new object[] {DslHelpers.CreateVariationMetric()};
        yield return new object[] {DslHelpers.CreateAccumulatorMetric()};
        yield return new object[] {DslHelpers.CreateStateMonitoringMetric()};
    }

    [TestCaseSource(nameof(_withoutWindowOfCases))]
    public void WhenIDoNotUseOnAWindowOf_ThenMetricWindowPeriodIsNone(IMetricDefinition<MetricUrn> standardMetric)
    {
        var sut = standardMetric.ToSemantic();
        Check.That(sut.WindowPolicy).IsEqualTo(Option<WindowPolicy>.None());
    }

    [Test]
    public void GivenStateMonitoringOf_WhenIUseOnAWindowOf_ThenInputTypeMustBeSet()
    {
        var sut = CreateMetricWithWindowOf(mmodel.metrics.state_monitoring_with_detailed_data, 10, TimeUnit.Seconds)
            .StateMonitoringOf(mmodel.measures.some_other_state)
            .ToSemantic();

        Check.That(sut.InputType.IsSome).IsTrue();
        Check.That(sut.InputType.GetValue()).IsEqualTo(typeof(TheState));
    }

    #region Helpers

    private static WindowedMetric CreateMetricWithWindowOf(MetricUrn metricUrn, int multiplier, TimeUnit timeUnit)
    {
        var metricBase = Metric(metricUrn).Is.Hourly;

        return timeUnit switch
        {
            TimeUnit.Milliseconds => metricBase.OnAWindowOf(multiplier).Milliseconds,
            TimeUnit.Seconds => metricBase.OnAWindowOf(multiplier).Seconds,
            TimeUnit.Minutes => metricBase.OnAWindowOf(multiplier).Minutes,
            TimeUnit.Hours => metricBase.OnAWindowOf(multiplier).Hours,
            TimeUnit.Days => metricBase.OnAWindowOf(multiplier).Days,
            TimeUnit.Weeks => metricBase.OnAWindowOf(multiplier).Weeks,
            TimeUnit.Months => metricBase.OnAWindowOf(multiplier).Months,
            TimeUnit.Quarters => metricBase.OnAWindowOf(multiplier).Quarters,
            TimeUnit.Years => metricBase.OnAWindowOf(multiplier).Years,
            _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
        };
    }

    #endregion
}