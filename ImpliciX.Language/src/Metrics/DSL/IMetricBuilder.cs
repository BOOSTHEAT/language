using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public interface IMetricBuilder
{
    IMetricBuilder AddSubMetric(string subMetricName, MetricKind metricKind, Urn inputUrn);
    IMetricBuilder WithInputUrn(Urn inputUrn);
    IMetricBuilder WithInputUrn(Urn inputUrn, Type urnType);
    IMetricBuilder WithPublicationPeriod(TimeSpan publicationPeriod);
    IMetricBuilder WithMetricKind(MetricKind metricKind);
    IMetricBuilder WithStoragePolicy(int duration, TimeUnit unit);
    IMetricBuilder WithWindowPolicy(int timeUnitMultiplier, TimeUnit timeUnit);
    IMetricBuilder AddGroupPolicy(TimeSpan period, int timeUnitMultiplier, TimeUnit timeUnit);
    TMetric Build<TMetric>();
}