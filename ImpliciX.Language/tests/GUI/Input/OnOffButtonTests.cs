using System;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Input;

public class OnOffButtonTests : ScreensTests
{
  [Test]
  public void WhenICreateOnOff_ThenOnOffHasContentExpected()
  {
    var onOff = OnOff(PropertyUrn<Enum>.Build("myUrn"));
    Check.That(onOff.Urn).IsEqualTo(Urn.BuildUrn("myUrn"));

    var widget = (OnOffButtonWidget) onOff.CreateWidget();
    Check.That(GetAsInstanceOf<PropertyFeed>(widget.Value).Urn).IsEqualTo(Urn.BuildUrn("myUrn"));
  }
}