using ImpliciX.Language.Model;
namespace ImpliciX.Language.GUI
{
  public class PropertyValue<T> : Value
  {
    private readonly PropertyUrn<T> _property;
    public PropertyValue(PropertyUrn<T> property) => _property = property;
    public override Feed CreateFeed() => PropertyFeed.Subscribe(_property);
  }
}