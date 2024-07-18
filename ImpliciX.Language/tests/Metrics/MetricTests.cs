using System;
using System.Collections.Generic;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;
using TimeUnit = ImpliciX.Language.Model.TimeUnit;

namespace ImpliciX.Language.Tests.Metrics;

using static ImpliciX.Language.Metrics.Metrics;

public class MetricTests
{

    [Test]
    public void GivenMetricWithManyGroups_WhenSemanticModelIsBuilt_ThenEachGroupIsIndexedByItsUrn()
    {
        var metric = Metric(mmodel.metrics.electrical_index).Is.Minutely.GaugeOf(mmodel.measures.the_temperature)
            .Group.Every(1).Days
            .Group.Every(2).Minutes;
        var groupsUrns = metric.ToSemantic().GroupPoliciesUrns;
        Check.That(groupsUrns).IsEqualTo(new Dictionary<Urn, GroupPolicy>()
        {
            {
                Urn.BuildUrn(mmodel.metrics.electrical_index.Value, "_1Days"),
                new GroupPolicy(TimeSpan.FromDays(1), 1, TimeUnit.Days)
            },
            {
                Urn.BuildUrn(mmodel.metrics.electrical_index.Value, "_2Minutes"),
                new GroupPolicy(TimeSpan.FromMinutes(2), 2, TimeUnit.Minutes)
            }
        });
    }
}