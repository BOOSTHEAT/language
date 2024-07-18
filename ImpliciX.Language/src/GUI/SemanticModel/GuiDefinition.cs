using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
    public class GuiDefinition
    {
        public Dictionary<GuiNode, Screen> Screens;
        public Dictionary<GuiNode, ScreenGroup> ScreenGroups;
        public GuiNode StartupScreen;
        public GuiNode ScreenWhenNotConnected;
        public Size ScreenSize;
        public Assembly Assets;
        public Internationalization Internationalization;
        public ScreenSaver ScreenSaver;
        public string VirtualKeyboard;
    }
}