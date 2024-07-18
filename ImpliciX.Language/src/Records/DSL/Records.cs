using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Records;

public static class Records
{
  public static NamedRecord<T> Record<T>(RecordsNode<T> node) where T : ModelNode => new(node);
}