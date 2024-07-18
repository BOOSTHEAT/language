using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public abstract class TimeUnitFluentStep<TNext> : FluentStep
{
    internal TimeUnitFluentStep(IMetricBuilder builder) : base(builder)
    {
    }

    public TNext Milliseconds => NextFactory(TimeUnit.Milliseconds);
    public TNext Seconds => NextFactory(TimeUnit.Seconds);
    public TNext Minutes => NextFactory(TimeUnit.Minutes);
    public TNext Hours => NextFactory(TimeUnit.Hours);
    public TNext Days => NextFactory(TimeUnit.Days);
    public TNext Weeks => NextFactory(TimeUnit.Weeks);
    public TNext Months => NextFactory(TimeUnit.Months);
    public TNext Quarters => NextFactory(TimeUnit.Quarters);
    public TNext Years => NextFactory(TimeUnit.Years);

    protected abstract TNext NextFactory(TimeUnit timeUnit);
}