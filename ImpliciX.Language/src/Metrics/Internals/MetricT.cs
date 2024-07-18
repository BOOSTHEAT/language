#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Metrics.Internals;

public record StoragePolicy(int Duration, TimeUnit TimeUnit)
{
    public int Duration { get; } = Duration;
    public TimeUnit TimeUnit { get; } = TimeUnit;
}

public record WindowPolicy(int TimeUnitMultiplier, TimeUnit TimeUnit)
{
    public int TimeUnitMultiplier { get; } = TimeUnitMultiplier;
    public TimeUnit TimeUnit { get; } = TimeUnit;
}

public record GroupPolicy(TimeSpan Period, int TimeUnitMultiplier, TimeUnit TimeUnit, Option<StoragePolicy>? StoragePolicy = null)
{
    public Option<StoragePolicy> StoragePolicy { get; set; } = StoragePolicy ?? Option<StoragePolicy>.None();
    public TimeSpan Period { get; } = Period;
    public int TimeUnitMultiplier { get; } = TimeUnitMultiplier;
    public TimeUnit TimeUnit { get; } = TimeUnit;
    public string Name => $"_{TimeUnitMultiplier}{TimeUnit}";
}

public record Metric<T> : IMetric
{
    public Metric(
        MetricKind kind,
        T target,
        Urn targetUrn,
        Urn inputUrn,
        TimeSpan publicationPeriod,
        Type? inputType = null,
        IEnumerable<SubMetricDef>? subMetricDefs = null,
        StoragePolicy? storagePolicy = default,
        WindowPolicy? windowPolicy = null,
        IEnumerable<GroupPolicy>? groupPolicies = default)
    {
        Kind = kind;
        Target = target;
        TargetUrn = targetUrn;
        InputUrn = inputUrn;
        InputType = inputType ?? Option<Type>.None();
        PublicationPeriod = publicationPeriod;
        WindowPolicy = windowPolicy ?? Option<WindowPolicy>.None();
        StoragePolicy = storagePolicy != null
            ? Option<StoragePolicy>.Some(storagePolicy)
            : Option<StoragePolicy>.None();

        SubMetricDefs = subMetricDefs ?? Array.Empty<SubMetricDef>();
        GroupPolicies = groupPolicies ?? Array.Empty<GroupPolicy>();
        GroupPoliciesUrns = GroupPolicies.ToDictionary(it => Urn.BuildUrn(targetUrn, it.Name));
    }

    public Dictionary<Urn, GroupPolicy> GroupPoliciesUrns { get; }

    public IEnumerable<Urn> InputUrns()
        => SubMetricDefs
            .Select(s => s.InputUrn)
            .Prepend(InputUrn);

    public T Target { get; }
    public Urn TargetUrn { get; }
    public MetricKind Kind { get; }
    public Urn InputUrn { get; }
    public Option<Type> InputType { get; }
    public TimeSpan PublicationPeriod { get; }
    public Option<WindowPolicy> WindowPolicy { get; }
    public IEnumerable<SubMetricDef> SubMetricDefs { get; }
    public Option<StoragePolicy> StoragePolicy { get; }
    public IEnumerable<GroupPolicy> GroupPolicies { get; }
}