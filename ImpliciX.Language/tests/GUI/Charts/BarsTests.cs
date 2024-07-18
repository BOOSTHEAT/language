using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class BarsTests : ScreensTests
{
  [Test]
  public void BarChart()
  {
    var barChart = Chart.Bars(
      Of(CreateMeasure("root:data1")),
      Of(CreateProperty("root:data2"))
      ).Width(344).Height(283);
    var widget = (BarsWidget)barChart.CreateWidget();
    Assert.That(widget.Width, Is.EqualTo(344));
    Assert.That(widget.Height, Is.EqualTo(283));
    Assert.That(widget.Content.Length, Is.EqualTo(2));
    Assert.That(GetAsInstanceOf<MeasureFeed>(widget.Content[0].Value).Urn.Value, Is.EqualTo("root:data1"));
    Assert.That(GetAsInstanceOf<PropertyFeed>(widget.Content[1].Value).Urn.Value, Is.EqualTo("root:data2"));
  }
}