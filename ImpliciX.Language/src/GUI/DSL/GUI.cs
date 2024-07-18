using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
    public class GUI
    {
        private readonly Dictionary<GuiNode, AlignedBlock[]> _screens = new ();
        private readonly Dictionary<GuiNode, (ScreenGroup.GroupKind, GuiNode[])> _screenGroups = new ();
        private GuiNode _start;
        private GuiNode _screenWhenNotConnected;
        private readonly Internationalization _i18n = new ();
        private readonly ScreenSaver _screenSaver = new ();
        private Assembly _assets;
        private Size _size;
        private string _keyboard;

        public GUI Screen(GuiNode node, AlignedBlock[] blocks)
        {
            _screens[node] = blocks;
            return this;
        }

        public GroupFacade Group => new (this);

        public GUI StartWith(GuiNode start)
        {
            _start = start;
            return this;
        }

        public GUI ScreenSaver(TimeSpan screenSaverTimeout, GuiNode screenSaver)
        {
            _screenSaver.Screen = screenSaver;
            _screenSaver.Timeout = screenSaverTimeout;
            return this;
        }

        public GUI WhenNotConnected(GuiNode screenWhenNotConnected)
        {
            _screenWhenNotConnected = screenWhenNotConnected;
            return this;
        }

        public GUI Assets(Assembly assembly)
        {
            _assets = assembly;
            return this;
        }

        public GUI Translations(string translationsAssetPath)
        {
            _i18n.TranslationFilename = translationsAssetPath;
            return this;
        }

        public GUI Locale<T>(PropertyUrn<T> locale)
        {
            _i18n.Locale = PropertyFeed.Subscribe(locale);
            return this;
        }

        public GUI TimeZone<T>(PropertyUrn<T> timezone)
        {
            _i18n.TimeZone = PropertyFeed.Subscribe(timezone);
            return this;
        }

        public GUI ScreenSize(int width, int height)
        {
            _size = new Size(width, height);
            return this;
        }

        internal void AddScreenGroup(GuiNode group, ScreenGroup.GroupKind kind, GuiNode[] screens)
        {
            _screenGroups.Add(group, (kind, screens));
        }

        public GUI VirtualKeyboard(string keyboardDefinition)
        {
            _keyboard = keyboardDefinition;
            return this;
        }

        public GuiDefinition ToSemanticModel() =>
            new ()
            {
                Screens = _screens.ToDictionary(
                    x => x.Key,
                    x => new Screen {Widgets = x.Value.Select(b => b.CreateWidget())}
                ),
                ScreenGroups = _screenGroups.ToDictionary(
                    x => x.Key,
                    x => new ScreenGroup
                    {
                        Kind = x.Value.Item1,
                        Screens = x.Value.Item2
                    }
                ),
                StartupScreen = _start,
                ScreenSize = _size,
                Assets = _assets,
                Internationalization = _i18n,
                ScreenSaver = _screenSaver,
                ScreenWhenNotConnected = _screenWhenNotConnected,
                VirtualKeyboard = _keyboard
            };
    }
}