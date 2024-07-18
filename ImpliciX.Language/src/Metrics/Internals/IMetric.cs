using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Metrics.Internals;

public interface IMetric
{
    Urn TargetUrn { get; }
    MetricKind Kind { get; }
    Urn InputUrn { get; }
    TimeSpan PublicationPeriod { get; }
    string Key => MetricHelper.GetKey(InputUrn, Kind);
    IEnumerable<Urn> InputUrns();
    IEnumerable<SubMetricDef> SubMetricDefs { get; }
    Option<StoragePolicy> StoragePolicy { get; }
    IEnumerable<GroupPolicy> GroupPolicies { get; }
}

public static class MetricHelper
{
    public static string GetKey(Urn inputUrn, MetricKind metricKind)
        => $"{inputUrn}({(short) metricKind})";
}