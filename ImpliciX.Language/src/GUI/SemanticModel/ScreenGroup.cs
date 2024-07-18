using System.Collections.Generic;

namespace ImpliciX.Language.GUI;

public class ScreenGroup
{
  public IEnumerable<GuiNode> Screens;
  public GroupKind Kind;

  public enum GroupKind
  {
    HorizontalSwipe,
    VerticalSwipe
  }
}