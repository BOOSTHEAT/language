using System.Collections.Generic;
namespace ImpliciX.Language.GUI
{
  public class Composite : Widget
  {
    public IEnumerable<Widget> Content;
    public ArrangeAs Arrange = ArrangeAs.XY;
    public int? Spacing;

    public enum ArrangeAs
    {
      XY,
      Column,
      Row
    }
  }
}