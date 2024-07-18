using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

namespace ImpliciX.Language.Records;

public class FluentStep<T>  where T : ModelNode
{
  public IRecord Instance { get; }
  internal IRecord<T> InstanceT { get; }

  internal FluentStep(IRecord<T> instance)
  {
    Instance = instance;
    InstanceT = instance;
  }
}