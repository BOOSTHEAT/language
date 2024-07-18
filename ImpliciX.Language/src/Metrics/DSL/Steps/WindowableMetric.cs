using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class WindowableMetric : TimeUnitFluentStep<WindowedMetric>
{
    private readonly int _timeUnitMultiplier;
    private readonly Func<IMetricBuilder, WindowedMetric> _nextFactory;

    internal WindowableMetric(IMetricBuilder builder, Func<IMetricBuilder, WindowedMetric> nextFactory, int timeUnitMultiplier) : base(builder)
    {
        _nextFactory = nextFactory;
        _timeUnitMultiplier = timeUnitMultiplier;
    }

    protected override WindowedMetric NextFactory(TimeUnit timeUnit) => _nextFactory(Builder.WithWindowPolicy(_timeUnitMultiplier, timeUnit));
}