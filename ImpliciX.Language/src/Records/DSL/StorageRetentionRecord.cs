using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Records;

public class StorageRetentionRecord<T> where T : ModelNode
{
  internal StorageRetentionRecord(IRecord<T> instance, int retention)
  {
    instance.SetStorageRetention(retention);
    Snapshot = new SnapshotRecord<T>(instance);
  }

  public SnapshotRecord<T> Snapshot { get; }
}