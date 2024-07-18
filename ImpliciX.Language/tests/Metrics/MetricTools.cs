using ImpliciX.Language.Metrics;
using ImpliciX.Language.Metrics.Internals;

namespace ImpliciX.Language.Tests.Metrics;

public static class MetricTools
{
  public static Metric<T> ToSemantic<T>(this IMetricDefinition<T> definition) => definition.Builder.Build<Metric<T>>();
}