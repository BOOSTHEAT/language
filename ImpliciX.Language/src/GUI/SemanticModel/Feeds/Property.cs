using ImpliciX.Language.Model;
namespace ImpliciX.Language.GUI
{
  public class PropertyFeed : Node
  {
    protected PropertyFeed(Urn urn) : base(urn)
    {
    }
    public static PropertyFeed<T> Subscribe<T>(PropertyUrn<T> urn) => new PropertyFeed<T>(urn);
  }

  public class PropertyFeed<T> : PropertyFeed
  {
    public PropertyFeed(PropertyUrn<T> urn) : base(urn)
    {
    }
  }
}