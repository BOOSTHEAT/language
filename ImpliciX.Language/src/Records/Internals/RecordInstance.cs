using System;
using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Records.Internals;

internal interface IRecord<T> : IRecord
  where T : ModelNode
{
  void SetWriters(IEnumerable<RecordWriterNode<T>> node);
  void SetStorageRetention(int retention);
}

internal class RecordInstance<T> : IRecord<T> where T : ModelNode
{
  public RecordInstance(RecordsNode<T> records)
  {
    Urn = records.Urn;
  }

  public void SetWriters(IEnumerable<RecordWriterNode<T>> writers)
  {
    Writers = writers.Select(w => (w.form.Urn, w.write)).ToList().AsReadOnly();
  }

  public void SetStorageRetention(int retention) => Retention = retention;

  public Urn Urn { get; }

  public Option<int> Retention { get; private set; } = Option<int>.None();

  public IReadOnlyList<(Urn, CommandUrn<NoArg>)> Writers { get; private set; }
  public Type Type => typeof(T);
}