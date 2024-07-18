
// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public interface IMetricDefinition
{
  IMetricBuilder Builder { get; }
}

public interface IMetricDefinition<T> : IMetricDefinition
{
}
