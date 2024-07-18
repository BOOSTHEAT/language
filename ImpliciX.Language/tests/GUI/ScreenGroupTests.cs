using System.Linq;
using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI;

public class ScreenGroupTests : ScreensTests
{
  [Test]
  public void HorizontalSwipe()
  {
    var screens = Enumerable.Range(1, 3).Select(i => CreateGuiNode($"root:screen{i}")).ToArray();
    var swipe = CreateGuiNode("root:swipe");
    var gui = GUI
      .Screen(screens[0], new AlignedBlock[] { })
      .Screen(screens[1], new AlignedBlock[] { })
      .Screen(screens[2], new AlignedBlock[] { })
      .Group.HorizontalSwipe(swipe, screens[0], screens[1], screens[2]);
    var sm = gui.ToSemanticModel();
    Assert.That(sm.ScreenGroups.Count, Is.EqualTo(1));
    Assert.That(sm.ScreenGroups.First().Key, Is.EqualTo(swipe));
    Assert.That(sm.ScreenGroups.First().Value.Kind, Is.EqualTo(ScreenGroup.GroupKind.HorizontalSwipe));
    Assert.That(sm.ScreenGroups.First().Value.Screens, Is.EqualTo(screens));
  }

  [Test]
  public void VerticalSwipe()
  {
    var screens = Enumerable.Range(1, 4).Select(i => CreateGuiNode($"root:screen{i}")).ToArray();
    var swipe = CreateGuiNode("root:swipe");
    var gui = GUI
      .Screen(screens[0], new AlignedBlock[] { })
      .Screen(screens[1], new AlignedBlock[] { })
      .Screen(screens[2], new AlignedBlock[] { })
      .Group.VerticalSwipe(swipe, screens[0], screens[1], screens[2], screens[3]);
    var sm = gui.ToSemanticModel();
    Assert.That(sm.ScreenGroups.Count, Is.EqualTo(1));
    Assert.That(sm.ScreenGroups.First().Key, Is.EqualTo(swipe));
    Assert.That(sm.ScreenGroups.First().Value.Kind, Is.EqualTo(ScreenGroup.GroupKind.VerticalSwipe));
    Assert.That(sm.ScreenGroups.First().Value.Screens, Is.EqualTo(screens));
  }
}