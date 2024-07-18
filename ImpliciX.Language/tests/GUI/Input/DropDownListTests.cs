using System;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Input;

public class DropDownListTests : ScreensTests
{
  [Test]
  public void WhenICreateDropDownList_ThenDropDownListHasContentExpected()
  {
    var dropDown = DropDownList(PropertyUrn<Enum>.Build("myUrn"));
    Check.That(dropDown.Urn).IsEqualTo(Urn.BuildUrn("myUrn"));

    var widget = (DropDownListWidget) dropDown.CreateWidget();
    Check.That(GetAsInstanceOf<PropertyFeed>(widget.Value).Urn).IsEqualTo(Urn.BuildUrn("myUrn"));
  }
  
  [Test]
  public void NoWidthOnDropDownListWidget()
  {
    var dropDown = DropDownList(PropertyUrn<Enum>.Build("myUrn")).CreateWidget();
    Check.That(dropDown.Width).IsNull();
  }
  
  [Test]
  public void WidthOnDropDownListWidget()
  {
    var dropDown = DropDownList(PropertyUrn<Enum>.Build("myUrn")).Width(500).CreateWidget();
    Check.That(dropDown.Width).IsEqualTo(500);
  }
}