using ImpliciX.Language.Model;
using ImpliciX.Language.Records.Internals;

namespace ImpliciX.Language.Records;

public class CompleteRecord<T> : FluentStep<T>, IRecordDefinition
  where T : ModelNode
{
  internal CompleteRecord(IRecord<T> instance) : base(instance)
  {
  }
}