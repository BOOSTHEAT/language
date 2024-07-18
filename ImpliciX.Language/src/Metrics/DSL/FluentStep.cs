
// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Metrics;

public class FluentStep : IMetricDefinition
{
  public IMetricBuilder Builder { get; }

  internal FluentStep(IMetricBuilder builder)
  {
    Builder = builder;
  }
}