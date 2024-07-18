using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

namespace ImpliciX.Language.Records;

public class NamedRecord<T> : FluentStep<T>
  where T : ModelNode
{
  internal NamedRecord(RecordsNode<T> node) : base(new RecordInstance<T>(node))
  {
    Is = new StartedRecord<T>(InstanceT);
  }

  public StartedRecord<T> Is { get; }
}