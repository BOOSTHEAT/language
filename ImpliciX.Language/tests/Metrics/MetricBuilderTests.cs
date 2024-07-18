#nullable enable
using System;
using System.Collections.Generic;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Metrics
{
    public class MetricBuilderTests
    {
        private static object[] _isCompleteFalseCases =
        {
            new object?[] { null, null, null, new[] { "metricKind=;inputUrn=;publicationPeriod=;" } },
            new object?[] { MetricKind.Variation, Urn.BuildUrn(""), TimeSpan.FromDays(1), new[] { "metricKind=Variation;inputUrn=;publicationPeriod=1.00:00:00;" } },
            new object?[]
            {
                MetricKind.Variation, Urn.BuildUrn("mmodel:metrics:device_communications"), null,
                new[] { "metricKind=Variation;inputUrn=mmodel:metrics:device_communications;publicationPeriod=;" }
            },
            new object?[] { MetricKind.Variation, null, TimeSpan.FromDays(1), new[] { "metricKind=Variation;inputUrn=;publicationPeriod=1.00:00:00;" } },
            new object?[] { MetricKind.Variation, Urn.BuildUrn(""), TimeSpan.FromDays(1), new[] { "metricKind=Variation;inputUrn=;publicationPeriod=1.00:00:00;" } }
        };

        [TestCaseSource(nameof(_isCompleteFalseCases))]
        public void GivenNotEnoughCompleted_WhenIBuild_ThenIGetAnException(MetricKind? metricKind, Urn? inputUrn, TimeSpan? publicationPeriod,
            IEnumerable<string> expectedExMessageContains)
        {
            var target = new AnalyticsCommunicationCountersNode("name", null!);
            var sut = new MetricBuilder<AnalyticsCommunicationCountersNode>(target);

            if (metricKind.HasValue)
                sut.WithMetricKind(metricKind.Value);
            if (publicationPeriod.HasValue)
                sut.WithPublicationPeriod(publicationPeriod.Value);
            if (inputUrn is not null)
                sut.WithInputUrn(inputUrn);

            var exception = Check.ThatCode(() => sut.Build<Metric<AnalyticsCommunicationCountersNode>>())
                .Throws<InvalidOperationException>()
                .Value;

            foreach (var containExpected in expectedExMessageContains)
            {
                Check.That(exception.Message).Contains(containExpected);
            }
        }

        [Test]
        public void GivenEnoughCompleted_WhenIBuild_ThenIGetTrue()
        {
            var target = new AnalyticsCommunicationCountersNode("name", null!);
            var sut = new MetricBuilder<AnalyticsCommunicationCountersNode>(target)
                .WithMetricKind(MetricKind.Communication)
                .WithInputUrn("mmodel:metrics:device_communications")
                .WithPublicationPeriod(TimeSpan.FromHours(2));

            var metric = sut.Build<Metric<AnalyticsCommunicationCountersNode>>();

            Check.That(metric.Kind).IsEqualTo(MetricKind.Communication);
            Check.That(metric.InputUrn.ToString()).IsEqualTo("mmodel:metrics:device_communications");
            Check.That(metric.PublicationPeriod).IsEqualTo(TimeSpan.FromHours(2));
        }
    }
}