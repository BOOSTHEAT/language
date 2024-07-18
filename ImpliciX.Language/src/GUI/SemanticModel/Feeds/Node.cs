using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
  public class Node : Feed
  {
    protected Node(Urn urn)
    {
      Urn = urn;
    }

    public Urn Urn { get; }
  }
}