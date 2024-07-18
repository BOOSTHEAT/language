using System;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class MetricOver<TNext> : FluentStep
{
  private readonly Func<IMetricBuilder, TNext> _nextFactory;

  internal MetricOver(IMetricBuilder builder, Func<IMetricBuilder, TNext> nextFactory) : base(builder)
  {
    _nextFactory = nextFactory;
  }

  public StorableMetric<TNext> ThePast(int multiplier) => new(Builder, _nextFactory, multiplier);
}