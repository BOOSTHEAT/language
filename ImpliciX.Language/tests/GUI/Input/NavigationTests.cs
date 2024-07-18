using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Input
{

  public class NavigationTests : ScreensTests
  {
    [Test]
    public void CreateNavigableImage()
    {
      var fooBar = CreateGuiNode("foo:bar");
      var selectable = CreateBlock();
      var selected = CreateBlock();
      var navigableBlock = selectable.NavigateTo(fooBar, selected);
      var widget = navigableBlock.CreateWidget();
      var navigator = GetAsInstanceOf<NavigatorWidget>(widget);
      Assert.That(navigator.TargetScreen, Is.EqualTo(fooBar));
      Assert.That(navigator.Visual, Is.EqualTo(selectable.CreateWidget()));
      Assert.That(navigator.OnTarget, Is.EqualTo(selected.CreateWidget()));
    }
  }
}