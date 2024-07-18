using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Metrics.Internals;

internal sealed class MetricBuilder<TTarget> : IMetricBuilder
{
    internal TTarget Target { get; }
    internal Option<TimeSpan> PublicationPeriod { get; private set; }
    internal Option<WindowPolicy> WindowPolicy { get; private set; }
    public Option<Urn> InputUrn { get; private set; }
    internal Option<Type> InputType { get; private set; }
    private Option<MetricKind> MetricKind { get; set; }
    internal List<SubMetricDef> SubMetricDefs { get; }
    private Option<StoragePolicy> StoragePolicy { get; set; }
    private List<GroupPolicy> GroupPolicies { get; }

    public MetricBuilder([DisallowNull] TTarget target)
    {
        Target = target ?? throw new ArgumentNullException(nameof(target));
        PublicationPeriod = Option<TimeSpan>.None();
        InputUrn = Option<Urn>.None();
        InputType = Option<Type>.None();
        MetricKind = Option<MetricKind>.None();
        SubMetricDefs = new List<SubMetricDef>();
        StoragePolicy = Option<StoragePolicy>.None();
        WindowPolicy = Option<WindowPolicy>.None();
        GroupPolicies = new List<GroupPolicy>();
    }

    public IMetricBuilder AddSubMetric(string subMetricName, MetricKind metricKind, Urn inputUrn)
    {
        SubMetricDefs.Add(new SubMetricDef(subMetricName, metricKind, inputUrn));
        return this;
    }

    public IMetricBuilder WithInputUrn(Urn inputUrn)
    {
        InputUrn = inputUrn ?? throw new ArgumentNullException(nameof(inputUrn));
        return this;
    }

    public IMetricBuilder WithInputUrn(Urn inputUrn, Type urnType)
    {
        WithInputUrn(inputUrn);
        InputType = urnType;
        return this;
    }

    public IMetricBuilder WithPublicationPeriod(TimeSpan publicationPeriod)
    {
        PublicationPeriod = publicationPeriod;
        return this;
    }

    public IMetricBuilder WithWindowPolicy(int timeUnitMultiplier, TimeUnit timeUnit)
    {
        WindowPolicy = new WindowPolicy(timeUnitMultiplier, timeUnit);
        return this;
    }

    public IMetricBuilder WithMetricKind(MetricKind metricKind)
    {
        MetricKind = metricKind;
        return this;
    }

    public IMetricBuilder WithStoragePolicy(int duration, TimeUnit unit)
    {
        if (GroupPolicies.Any())
        {
            GroupPolicies[^1] = GroupPolicies[^1] with {StoragePolicy = new StoragePolicy(duration, unit)};
        }
        else
        {
            StoragePolicy = new StoragePolicy(duration, unit);
        }

        return this;
    }

    public IMetricBuilder AddGroupPolicy(TimeSpan period, int timeUnitMultiplier, TimeUnit timeUnit)
    {
        GroupPolicies.Add(new GroupPolicy(period, timeUnitMultiplier, timeUnit, null));
        return this;
    }

    public METRIC Build<METRIC>() // where METRIC : Metric<TTarget>
    {
        var isEnoughForBuild = PublicationPeriod.IsSome && !string.IsNullOrEmpty(InputUrn.GetValueOrDefault("")) && MetricKind.IsSome;
        if (!isEnoughForBuild)
            throw new InvalidOperationException("All data must be completed. Please set data missing (" +
                                                $"target={Target};" +
                                                $"metricKind={GetOptionValueOrEmpty(MetricKind)};" +
                                                $"inputUrn={GetOptionValueOrEmpty(InputUrn)};" +
                                                $"publicationPeriod={GetOptionValueOrEmpty(PublicationPeriod)};" +
                                                ")");

        return Target switch
        {
            AnalyticsCommunicationCountersNode target => (METRIC) (object) new Metric<AnalyticsCommunicationCountersNode>(MetricKind.GetValue(),
                target, target.Urn, InputUrn.GetValue(),
                PublicationPeriod.GetValue(),
                InputType.GetValueOrDefault(null),
                subMetricDefs: SubMetricDefs,
                windowPolicy: WindowPolicy.GetValueOrDefault(null)),

            MetricUrn target => (METRIC) (object) new Metric<MetricUrn>(MetricKind.GetValue(),
                target, target, InputUrn.GetValue(),
                PublicationPeriod.GetValue(),
                InputType.GetValueOrDefault(null),
                subMetricDefs: SubMetricDefs,
                storagePolicy: StoragePolicy.GetValueOrDefault(null),
                windowPolicy: WindowPolicy.GetValueOrDefault(null),
                groupPolicies: GroupPolicies),

            _ => throw new ArgumentOutOfRangeException(nameof(Target), Target, null)
        };
    }

    private static string GetOptionValueOrEmpty<T>(Option<T> option)
        => option.Match(() => "", o => o!.ToString());
}