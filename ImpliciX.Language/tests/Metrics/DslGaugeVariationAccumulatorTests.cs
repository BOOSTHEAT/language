using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Metrics.Metrics;
using TimeUnit = ImpliciX.Language.Model.TimeUnit;

namespace ImpliciX.Language.Tests.Metrics;

public class DslGaugeVariationAccumulatorTests
{
  [Test]
  public void GivenValidInputData_WhenIDeviceMonitoringOf_ThenIGetMetric()
  {
    var metric = Metric(mmodel.metrics.device_communications)
      .Is
      .Minutely
      .DeviceMonitoringOf(mmodel.measures.the_device);

    Check.That(metric).InheritsFrom<IMetricDefinition>();
    var sut = metric.ToSemantic();
    Check.That(sut).InheritsFrom<IMetric>();

    Check.That(sut.Kind).IsEqualTo(MetricKind.Communication);
    Check.That(sut.Target.Token).IsEqualTo(nameof(mmodel.metrics.device_communications));
    Check.That(sut.Target.Urn).IsEqualTo(mmodel.metrics.device_communications.Urn);
    Check.That(sut.InputUrn).IsEqualTo(mmodel.measures.the_device.Urn);
    Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromMinutes(1));
  }

  [Test]
  public void WhenIGaugeOf()
  {
    var metric = Metric(mmodel.instrumentation.electrical_index)
      .Is
      .Hourly
      .GaugeOf(instrumentation.electrical_index.measure);

    Check.That(metric).InheritsFrom<IMetricDefinition>();
    var sut = metric.ToSemantic();
    Check.That(sut).InheritsFrom<IMetric>();

    Check.That(sut.Kind).IsEqualTo(MetricKind.Gauge);
    Check.That(sut.Target).IsEqualTo(mmodel.instrumentation.electrical_index);
    Check.That(sut.InputUrn.ToString()).IsEqualTo(instrumentation.electrical_index.measure);
    Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));
  }

  [Test]
  public void WhenIAccumulatorOf()
  {
    var metric = Metric(mmodel.production.main_circuit.supply_temperature)
      .Is
      .Daily
      .AccumulatorOf(production.main_circuit.supply_temperature.measure);

    Check.That(metric).InheritsFrom<IMetricDefinition>();
    var sut = metric.ToSemantic();
    Check.That(sut).InheritsFrom<IMetric>();

    Check.That(sut.Kind).IsEqualTo(MetricKind.SampleAccumulator);
    Check.That(sut.Target).IsEqualTo(mmodel.production.main_circuit.supply_temperature);
    Check.That(sut.InputUrn.ToString()).IsEqualTo(production.main_circuit.supply_temperature.measure);
    Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromDays(1));
  }

  [Test]
  public void WhenIVariationOf()
  {
    var metric = Metric(mmodel.instrumentation.electrical_index)
      .Is
      .Hourly
      .VariationOf(instrumentation.electrical_index.measure);

    Check.That(metric).InheritsFrom<IMetricDefinition>();
    var sut = metric.ToSemantic();
    Check.That(sut).InheritsFrom<IMetric>();

    Check.That(sut.Kind).IsEqualTo(MetricKind.Variation);
    Check.That(sut.Target).IsEqualTo(mmodel.instrumentation.electrical_index);
    Check.That(sut.InputUrn.ToString()).IsEqualTo(instrumentation.electrical_index.measure);
    Check.That(sut.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(1));
  }

  private static IEnumerable<object[]> BasePeriodCasesForGaugeVariationAccumulator(Func<TimeUnit, StandardMetric> createMetric)
  {
    yield return new object[] {createMetric(TimeUnit.Days), TimeSpan.FromDays(1)};
    yield return new object[] {createMetric(TimeUnit.Hours), TimeSpan.FromHours(1)};
    yield return new object[] {createMetric(TimeUnit.Minutes), TimeSpan.FromMinutes(1)};
    yield return new object[] {createMetric(TimeUnit.Seconds), TimeSpan.FromSeconds(1)};
    yield return new object[] {createMetric(TimeUnit.Milliseconds), TimeSpan.FromMilliseconds(1)};
  }

  public static IEnumerable<object[]> PeriodCasesForGaugeVariationAccumulator()
  {
    var metricFactories = new[]
    {
      new Func<TimeUnit, StandardMetric>(DslHelpers.CreateGaugeMetric),
      new Func<TimeUnit, StandardMetric>(DslHelpers.CreateVariationMetric),
      new Func<TimeUnit, StandardMetric>(DslHelpers.CreateAccumulatorMetric)
    };

    return metricFactories.SelectMany(BasePeriodCasesForGaugeVariationAccumulator);
  }

  [TestCaseSource(nameof(PeriodCasesForGaugeVariationAccumulator))]
  public void GivenGaugeVariationAccumulator_WhenIPeriodIsChosen_ThenIGetPeriodExpected(StandardMetric metric, TimeSpan periodExpected)
  {
    var sut = metric.ToSemantic();
    Check.That(sut.PublicationPeriod).IsEqualTo(periodExpected);
  }

  private static IEnumerable<object[]> MultipleGroupingCasesForGaugeVariationAccumulator(Func<StandardMetric> createMetric)
  {
    yield return new object[] {createMetric(), Enumerable.Empty<GroupPolicy>()};

    yield return new object[] {createMetric().Group.Daily, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Days)})};
    yield return new object[] {createMetric().Group.Hourly, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Hours)})};
    yield return new object[] {createMetric().Group.Minutely, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes)})};
    yield return new object[] {createMetric().Group.Every(1).Seconds, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Seconds)})};
    yield return new object[] {createMetric().Group.Every(1).Milliseconds, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Milliseconds)})};

    yield return new object[] {createMetric().Group.Daily.Group.Every(5).Days, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Days), (5, TimeUnit.Days)})};
    yield return new object[] {createMetric().Group.Hourly.Group.Every(10).Days, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Hours), (10, TimeUnit.Days)})};
    yield return new object[] {createMetric().Group.Minutely.Group.Every(15).Days, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (15, TimeUnit.Days)})};
    yield return new object[] {createMetric().Group.Every(1).Seconds.Group.Every(3).Days, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Seconds), (3, TimeUnit.Days)})};
    yield return new object[] {createMetric().Group.Every(1).Milliseconds.Group.Every(3).Days, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Milliseconds), (3, TimeUnit.Days)})};

    yield return new object[] {createMetric().Group.Daily.Group.Every(5).Hours, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Days), (5, TimeUnit.Hours)})};
    yield return new object[] {createMetric().Group.Hourly.Group.Every(10).Hours, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Hours), (10, TimeUnit.Hours)})};
    yield return new object[] {createMetric().Group.Minutely.Group.Every(15).Hours, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (15, TimeUnit.Hours)})};
    yield return new object[] {createMetric().Group.Every(1).Seconds.Group.Every(3).Hours, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Seconds), (3, TimeUnit.Hours)})};
    yield return new object[] {createMetric().Group.Every(1).Milliseconds.Group.Every(3).Hours, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Milliseconds), (3, TimeUnit.Hours)})};

    yield return new object[] {createMetric().Group.Daily.Group.Every(5).Minutes, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Days), (5, TimeUnit.Minutes)})};
    yield return new object[] {createMetric().Group.Hourly.Group.Every(10).Minutes, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Hours), (10, TimeUnit.Minutes)})};
    yield return new object[] {createMetric().Group.Minutely.Group.Every(15).Minutes, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (15, TimeUnit.Minutes)})};
    yield return new object[] {createMetric().Group.Every(1).Seconds.Group.Every(3).Minutes, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Seconds), (3, TimeUnit.Minutes)})};
    yield return new object[] {createMetric().Group.Every(1).Milliseconds.Group.Every(3).Minutes, DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Milliseconds), (3, TimeUnit.Minutes)})};

    yield return new object[]
    {
      createMetric().Group.Minutely.Group.Every(15).Days.Group.Every(6).Hours,
      DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (15, TimeUnit.Days), (6, TimeUnit.Hours)})
    };

    yield return new object[]
    {
      createMetric().Group.Minutely.Group.Every(15).Days.Group.Every(30).Minutes,
      DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (15, TimeUnit.Days), (30, TimeUnit.Minutes)})
    };

    yield return new object[]
    {
      createMetric().Group.Minutely.Group.Every(7).Days.Group.Every(30).Minutes.Group.Every(6).Hours,
      DslHelpers.CreateGroupPolicies(new[] {(1, TimeUnit.Minutes), (7, TimeUnit.Days), (30, TimeUnit.Minutes), (6, TimeUnit.Hours)})
    };
  }

  public static IEnumerable<object[]> MultipleGroupCasesForGaugeVariationAccumulator()
  {
    var metricFactories = new[]
    {
      () => DslHelpers.CreateGaugeMetric(),
      () => DslHelpers.CreateVariationMetric(),
      () => DslHelpers.CreateAccumulatorMetric()
    };

    return metricFactories.SelectMany(MultipleGroupingCasesForGaugeVariationAccumulator);
  }

  [TestCaseSource(nameof(MultipleGroupCasesForGaugeVariationAccumulator))]
  public void GivenGaugeVariationAccumulator_WhenIUseGroupSyntax_ThenIGetGroupPoliciesExpected(StandardMetric metric, IEnumerable<GroupPolicy> groupPoliciesExpected)
  {
    var groupPolicies = metric.ToSemantic().GroupPolicies;
    Check.That(groupPolicies).ContainsExactly(groupPoliciesExpected);
  }
}