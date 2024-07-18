using System.Drawing;
using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI
{

  public class FontTests : ScreensTests
  {
    [TestCase(10)]
    [TestCase(24)]
    public void SelectFontSize(int fontSize)
    {
      var font = Font.Size(fontSize);
      var style = font.CreateStyle();
      Assert.That(style.FontSize, Is.EqualTo(fontSize));
    }

    [TestCase(255, 106, 19)]
    [TestCase(113, 184, 219)]
    public void SelectFontColor(int red, int green, int blue)
    {
      var color = Color.FromArgb(red, green, blue);
      var font = Font.Color(color);
      var style = font.CreateStyle();
      Assert.That(style.FrontColor, Is.EqualTo(color));
    }

    [Test]
    public void SelectFontFamilyLight()
    {
      var font = Font.Light;
      var style = font.CreateStyle();
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.Light));
    }

    [Test]
    public void SelectFontFamilyMedium()
    {
      var font = Font.Medium;
      var style = font.CreateStyle();
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.Medium));
    }

    [Test]
    public void SelectFontFamilyExtraBold()
    {
      var font = Font.ExtraBold;
      var style = font.CreateStyle();
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.ExtraBold));
    }

    [Test]
    public void CreateLabelWithFont()
    {
      var labelBlock = Label("foo").With(Font.Medium.Size(16).Color(Color.Blue));
      var widget = labelBlock.CreateWidget();
      var style = GetAsInstanceOf<Style>(widget.Style);
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.Medium));
      Assert.That(style.FontSize, Is.EqualTo(16));
      Assert.That(style.FrontColor, Is.EqualTo(Color.Blue));
    }

    [Test]
    public void CreateTranslationWithFont()
    {
      var translationBlock = Translate("foo").With(Font.Medium.Size(16).Color(Color.Blue));
      var widget = translationBlock.CreateWidget();
      var style = GetAsInstanceOf<Style>(widget.Style);
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.Medium));
      Assert.That(style.FontSize, Is.EqualTo(16));
      Assert.That(style.FrontColor, Is.EqualTo(Color.Blue));
    }

    [Test]
    public void CreateMeasureWithFont()
    {
      var measureBlock = Show(CreateMeasure("foo:bar"))
        .With(Font.Medium.Size(16).Color(Color.Blue));
      var widget = measureBlock.CreateWidget();
      var style = GetAsInstanceOf<Style>(widget.Style);
      Assert.That(style.FontFamily, Is.EqualTo(Style.Family.Medium));
      Assert.That(style.FontSize, Is.EqualTo(16));
      Assert.That(style.FrontColor, Is.EqualTo(Color.Blue));
    }
  }
}