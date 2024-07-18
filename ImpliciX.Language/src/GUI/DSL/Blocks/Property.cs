using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
  public class Property<T> : Block
  {
    internal PropertyUrn<T> Urn { get; set; }

    public override Widget CreateWidget()
    {
      return new Text
      {
        Value = PropertyFeed.Subscribe(Urn),
        Style = Font?.CreateStyle(),
        Width = WidthValue
      };
    }
  }
}