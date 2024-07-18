using System;

namespace ImpliciX.Language.Model;

public class RecordsNode<T> : ModelNode
  where T: ModelNode
{
  public RecordsNode(string name, ModelNode parent) : base(name,parent)
  {
  }
}

public class RecordWriterNode<T> : ModelNode
  where T: ModelNode
{
  public RecordWriterNode(Urn urnToken, ModelNode parent, Func<string,ModelNode,T> build)
    : base(urnToken, parent)
  {
    form = build(nameof(form), this);
    write = CommandUrn<NoArg>.Build(Urn, nameof(write));
  }

  public T form { get; }
  public CommandUrn<NoArg> write { get; }

}