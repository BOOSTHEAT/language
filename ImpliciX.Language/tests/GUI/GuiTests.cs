using System;
using System.Linq;
using System.Reflection;
using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI
{
  public class GuiTests : ScreensTests
  {
    [Test]
    public void CreateGui()
    {
      var screen1Node = CreateGuiNode("root:screen1");
      var screen1 = Screen(CreateAlignedBlock(), CreateAlignedBlock());
      var screen2Node = CreateGuiNode("root:screen2");
      var screen2 = Screen(CreateAlignedBlock(), CreateAlignedBlock(), CreateAlignedBlock());
      var screenSaverNode = CreateGuiNode("root:screen_saver");
      var screenWhenNotConnected = CreateGuiNode("root:screen_when_not_connected");
      var localeProperty = CreateProperty("root:locale");
      var timezoneProperty = CreateProperty("root:timezone");

      var gui = GUI
        .Screen(screen1Node, screen1)
        .Screen(screen2Node, screen2)
        .StartWith(screen1Node)
        .WhenNotConnected(screenWhenNotConnected)
        .ScreenSaver(TimeSpan.FromMinutes(10), screenSaverNode)
        .Assets(Assembly.GetExecutingAssembly())
        .Translations("translations.csv")
        .Locale(localeProperty)
        .TimeZone(timezoneProperty)
        .VirtualKeyboard("my_fancy_keyboard");

      var guiDefinition = gui.ToSemanticModel();

      Assert.That(guiDefinition.Screens.Count, Is.EqualTo(2));
      Assert.That(guiDefinition.Screens.First().Key, Is.EqualTo(screen1Node));
      Assert.That(guiDefinition.Screens.First().Value.Widgets, Is.EqualTo(screen1.Select(b => b.CreateWidget())));
      Assert.That(guiDefinition.Screens.Last().Key, Is.EqualTo(screen2Node));
      Assert.That(guiDefinition.Screens.Last().Value.Widgets, Is.EqualTo(screen2.Select(b => b.CreateWidget())));
      Assert.That(guiDefinition.StartupScreen, Is.EqualTo(screen1Node));

      Assert.That(guiDefinition.Assets, Is.EqualTo(Assembly.GetExecutingAssembly()));

      Assert.That(guiDefinition.Internationalization.TranslationFilename, Is.EqualTo("translations.csv"));
      Assert.That(guiDefinition.ScreenSaver.Screen, Is.EqualTo(screenSaverNode));
      Assert.That(guiDefinition.ScreenSaver.Timeout, Is.EqualTo(TimeSpan.FromMinutes(10)));
      Assert.That(guiDefinition.ScreenWhenNotConnected, Is.EqualTo(screenWhenNotConnected));
      var locale = GetAsInstanceOf<PropertyFeed>(guiDefinition.Internationalization.Locale);
      Assert.That(locale.Urn.Value, Is.EqualTo("root:locale"));
      var timezone = GetAsInstanceOf<PropertyFeed>(guiDefinition.Internationalization.TimeZone);
      Assert.That(timezone.Urn.Value, Is.EqualTo("root:timezone"));
      
      Assert.That(guiDefinition.VirtualKeyboard, Is.EqualTo("my_fancy_keyboard"));
    }
  }
}