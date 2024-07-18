using System.Linq;

namespace ImpliciX.Language.GUI;

public class GroupFacade
{
  private readonly GUI _gui;

  public GroupFacade(GUI gui)
  {
    _gui = gui;
  }

  public GUI HorizontalSwipe(GuiNode swipe, GuiNode screen1, GuiNode screen2, params GuiNode[] screens)
  {
    _gui.AddScreenGroup(swipe, ScreenGroup.GroupKind.HorizontalSwipe, screens.Prepend(screen2).Prepend(screen1).ToArray());
    return _gui;
  }

  public GUI VerticalSwipe(GuiNode swipe, GuiNode screen1, GuiNode screen2, params GuiNode[] screens)
  {
    _gui.AddScreenGroup(swipe, ScreenGroup.GroupKind.VerticalSwipe, screens.Prepend(screen2).Prepend(screen1).ToArray());
    return _gui;
  }
}