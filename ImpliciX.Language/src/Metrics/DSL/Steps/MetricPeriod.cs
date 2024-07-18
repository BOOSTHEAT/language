using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public sealed class MetricPeriod<TNext> : FluentStep
{
  private readonly Func<IMetricBuilder, TimeSpan, int, TimeUnit, TNext> _nextFactory;

  /// <summary>
  /// Multiply the next time chosen (.Hourly, .Daily ...) by the multiplier put on input parameter
  /// </summary>
  /// <param name="multiplier">Must be greater than 0</param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException">If input parameter is not greater than 0</exception>
  public MetricTimeUnit<TNext> Every(int multiplier)
  {
    return multiplier > 0
      ? new MetricTimeUnit<TNext>(
        Builder,
        (builder, timeSpan, _, timeUnit) => _nextFactory(builder, multiplier * timeSpan, multiplier, timeUnit)
        )
      : throw new InvalidOperationException(
        $"{nameof(Every)} input parameter ({multiplier}) is invalid : It must be greater than 0");
  }

  public TNext Daily => Every(1).Days;
  public TNext Minutely => Every(1).Minutes;
  public TNext Hourly => Every(1).Hours;

  internal MetricPeriod(IMetricBuilder builder, Func<IMetricBuilder, TimeSpan, int, TimeUnit, TNext> nextFactory) : base(builder)
  {
    _nextFactory = nextFactory ?? throw new ArgumentNullException(nameof(nextFactory));
  }
}