using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class MetricTimeUnit<TNext> : FluentStep
{
  private readonly Func<IMetricBuilder, TimeSpan, int, TimeUnit, TNext> _nextFactory;

  public TNext Days => Make(TimeSpan.FromDays(1), TimeUnit.Days);
  public TNext Hours => Make(TimeSpan.FromHours(1), TimeUnit.Hours);
  public TNext Minutes => Make(TimeSpan.FromMinutes(1), TimeUnit.Minutes);
  public TNext Seconds => Make(TimeSpan.FromSeconds(1), TimeUnit.Seconds);
  public TNext Milliseconds => Make(TimeSpan.FromMilliseconds(1), TimeUnit.Milliseconds);
  private TNext Make(TimeSpan timeSpan, TimeUnit timeUnit) => _nextFactory(Builder, timeSpan, 1, timeUnit);

  internal MetricTimeUnit(IMetricBuilder builder, Func<IMetricBuilder, TimeSpan, int, TimeUnit, TNext> nextFactory)
    : base(builder)
  {
    _nextFactory = nextFactory ?? throw new ArgumentNullException(nameof(nextFactory));
  }
}