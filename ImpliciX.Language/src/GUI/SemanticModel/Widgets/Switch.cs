using System.Collections.Generic;

namespace ImpliciX.Language.GUI
{
  public class SwitchWidget : Widget
  {
    public IEnumerable<Case> Cases;
    public Widget Default;

    public class Case
    {
      public BinaryExpression When;
      public Widget Then;
    }
 
  }
}