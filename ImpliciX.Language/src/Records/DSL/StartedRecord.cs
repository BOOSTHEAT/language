using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Records;

public class StartedRecord<T> : FluentStep<T>
  where T : ModelNode
{
  internal StartedRecord(IRecord<T> instance) : base(instance)
  {
    Snapshot = new SnapshotRecord<T>(instance);
  }

  public SnapshotRecord<T> Snapshot { get; }

  public StorageRetentionRecord<T> Last(int retention) => new (InstanceT, retention);
}