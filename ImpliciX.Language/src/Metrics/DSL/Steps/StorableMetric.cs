using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class StorableMetric<TNext> : TimeUnitFluentStep<TNext>
{
    private readonly int _duration;
    private readonly Func<IMetricBuilder, TNext> _nextFactory;

    internal StorableMetric(IMetricBuilder builder, Func<IMetricBuilder, TNext> nextFactory, int duration) : base(builder)
    {
        _nextFactory = nextFactory;
        _duration = duration;
    }

    protected override TNext NextFactory(TimeUnit unit) => _nextFactory(Builder.WithStoragePolicy(_duration, unit));
}