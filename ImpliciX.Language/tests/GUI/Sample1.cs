using System.Drawing;
using System.Linq;
using System.Reflection;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NUnit.Framework;
#pragma warning disable CS8981

namespace ImpliciX.Language.Tests.GUI
{

  public class Sample1 : Screens
  {
  // @formatter:off
  private static Color darkGrey = Color.FromArgb(51,51,51);
  private static Color lighterGrey = Color.FromArgb(240,240,240);
  private static Color orange = Color.FromArgb(255,106,19);
  
  private static Block date = Column.Layout(
    Now.WeekDay,
    Now.Date,
    Now.HoursMinutesSeconds
    ).With(Font.Light.Size(26).Color(darkGrey));
  
  private static Block houseBackground = Canvas.Layout(
    At.Origin.Put( Image("assets/home/home_isometric-shower.png") ),
    At.Origin.Put( Image("assets/home/home_isometric-empty.png") ),
    At.Origin.Put( Image("assets/home/home_isometric-bh20.png") )
  );
  
  private static Block meteo =
    Background(Image("assets/home/meteo.png")).Layout(
      At.Right(14).Bottom(8).Put(
        Row.Layout(
          Show(root.outdoor_temperature).With(Font.Medium),
          Label("°C").With(Font.Light)
        ).With(Font.Size(25).Color(orange))
      )
    );
  
  private static Font _pressureFont = Font.ExtraBold.Size(22);
  private static Block pressure = Show(root.supply_pressure);
  private static AlignedBlock.Fully atPressurePosition = At.Right(23).Bottom(2);
  private static AlignedBlock barLabel = At.Right(6).Bottom(6).Put(
    Label("bar").With(Font.Light.Size(10).Color(darkGrey))
  );
  
  private static Block pressurePicto = Column.Layout(
    Switch
      .Case(Value(root.supply_pressure) < 0.95f,
        Background(Image("assets/home/btn-pressure-low.png")).Layout(
          atPressurePosition.Put(pressure.With(_pressureFont.Color(orange))),
          barLabel
        )
      )
      .Default(
        Background(Image("assets/home/btn-pressure-ok.png")).Layout(
          atPressurePosition.Put(pressure.With(_pressureFont.Color(darkGrey))),
          barLabel
        )
      ),
    Translate("Water_Pressure").Width(62).With(Font.Light.Size(13))
  );
  
  private static Block selected = Box.Radius(12).Border(darkGrey);

  private static Block menu = Background(Box.Width(84).Height(480).Fill(lighterGrey)).Layout(
    At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(
      Column.Spacing(16).Layout(
          Image("assets/menu/home.png").NavigateTo(root.screen1,selected),
          Image("assets/menu/shower_eco_running.gif").NavigateTo(root.screen2,selected)
        )
      )
    );

  private static AlignedBlock.Fully atHousePosition = At.Left(166).Top(41);
  
  private static AlignedBlock[] screen1 = Screen(
    At.Top(10).Left(14).Put(date),
      atHousePosition.Put(houseBackground),
      atHousePosition.Put( Image("assets/home/floor2.png") ),
      atHousePosition.Put( Image("assets/home/home_isometric-wall.png") ),
      atHousePosition.Put( Image("assets/home/floor1.png") ),
      atHousePosition.Put( Image("assets/home/home_isometric-EU-off.png") ),
      At.Left(175).Top(12).Put(meteo),
      At.Left(35).Top(259).Put(pressurePicto),
      At.Right(0).Top(0).Put(menu)
    );

  private static AlignedBlock[] screen2 = Screen(
    At.Top(10).Left(14).Put(date),
      atHousePosition.Put(houseBackground),
      atHousePosition.Put( Image("assets/home/floor2.png") ),
      atHousePosition.Put( Image("assets/home/home_isometric-wall.png") ),
      atHousePosition.Put( Image("assets/home/floor1.png") ),
      atHousePosition.Put( Image("assets/home/home_isometric-EU-off.png") ),
      At.Left(175).Top(12).Put(meteo),
      At.Left(35).Top(259).Put(pressurePicto),
      At.Right(0).Top(0).Put(menu)
    );

  public static ImpliciX.Language.GUI.GUI Gui = GUI
    .Screen(root.screen1, screen1)
    .Screen(root.screen2, screen2)
    .StartWith(root.screen1)
    .Assets(Assembly.GetExecutingAssembly())
    .Translations("translations.csv")
    .Locale(root.locale)
    .TimeZone(root.timezone)
    .ScreenSize(3840,2160);
  
    // @formatter:on

    [Test]
    public void GenerateSemanticModel()
    {
      var model = Gui.ToSemanticModel();
      var lastWidgetOfScreen1 = (Composite)model.Screens[root.screen1].Widgets.Last();
      Assert.That(lastWidgetOfScreen1.Content.First().Style.BackColor, Is.EqualTo(lighterGrey));
    }
    
    class root : RootModelNode
    {
      public root() : base(nameof(root))
      {
      }

      static root()
      {
        var root = new root();
        screen1 = new GuiNode(root, nameof(screen1));
        screen2 = new GuiNode(root, nameof(screen2));
        language = UserSettingUrn<Language>.Build(nameof(root), nameof(language));
        locale = UserSettingUrn<Locale>.Build(nameof(root), nameof(locale));
        timezone = UserSettingUrn<TimeZone>.Build(nameof(root), nameof(timezone)); 
        supply_pressure = new MeasureNode<Pressure>(nameof(supply_pressure), root);
        outdoor_temperature = new MeasureNode<Temperature>(nameof(outdoor_temperature), root);
      }

      public static GuiNode screen1 { get; }
      public static GuiNode screen2 { get; }
      public static UserSettingUrn<Language> language { get; } 
      public static UserSettingUrn<Locale> locale { get; } 
      public static UserSettingUrn<TimeZone> timezone{ get; } 
      public static MeasureNode<Pressure> supply_pressure { get; }
      public static MeasureNode<Temperature> outdoor_temperature { get; }
    }
    
    public enum Language
    {
      fr = 2,
      de = 3,
      en = 0
    }
    
    public enum Locale
    {
      fr__FR = 2,
      de__DE = 3,
      en__GB = 1,
      en__US = 0
    }
    
    public enum TimeZone
    {
      Europe__Paris,
      Europe__London,
      America__New_York,
      Asia__Tokyo,
    }
  }
}
