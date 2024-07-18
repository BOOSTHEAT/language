using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Input;

public class InputTests : ScreensTests
{
  [Test]
  public void NominalCase()
  {
    var inputBlock = Input(CreateProperty<Literal>("myUrn"));
    var widget = (TextBox)inputBlock.CreateWidget();
    Check.That(GetAsInstanceOf<PropertyFeed>(widget.Value).Urn.Value).IsEqualTo("myUrn");
  }

  [Test]
  public void WithWidth()
  {
    var inputBlock = Input(CreateProperty<Literal>("myUrn")).Width(400);
    var widget = (TextBox) inputBlock.CreateWidget();
    Check.That(widget.Width).IsEqualTo(400);
  }

  [Test]
  public void WithFont()
  {
    var inputBlock = Input(CreateProperty<Literal>("myUrn")).With(Font.ExtraBold.Size(22));
    var widget = (TextBox) inputBlock.CreateWidget();
    Check.That(widget.Style.FontFamily).IsEqualTo(Style.Family.ExtraBold);
    Check.That(widget.Style.FontSize).IsEqualTo(22);
  }
}