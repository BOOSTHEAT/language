using System.Linq;
using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

namespace ImpliciX.Language.Records;

public class SnapshotRecord<T> : FluentStep<T>
  where T : ModelNode
{
  internal SnapshotRecord(IRecord<T> instance) : base(instance)
  {
  }

  public CompleteRecord<T> Of(RecordWriterNode<T> writer, params RecordWriterNode<T>[] additionalWriters)
  {
    InstanceT.SetWriters(additionalWriters.Prepend(writer));
    var cr = new CompleteRecord<T>(InstanceT);
    return cr;
  }
}