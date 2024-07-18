using System.Collections.Generic;
using System.Linq;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class MultiTests : ScreensTests
{
  [Test]
  public void TwoTimeLineChartsWithDifferentYAxis()
  {
    var urn1 = Urn.BuildUrn("urn1");
    var urn2 = Urn.BuildUrn("urn2");
    
    var chart = Chart.Multi(
      Chart.TimeLines(Of(urn1)),
      Chart.TimeLines(Of(urn2))
    ).Width(200).Height(100);

    var widget = chart.CreateWidget();
    
    Assert.That(widget.Width, Is.EqualTo(200));
    Assert.That(widget.Height, Is.EqualTo(100));


    Assert.That(widget, Is.InstanceOf<MultiChartWidget>());
    var mcWidget = (MultiChartWidget)widget;
    
    Assert.That(mcWidget.Left, Is.InstanceOf<TimeLinesWidget>());
    Assert.That(UrnsOf(mcWidget.Left).First(), Is.EqualTo(urn1));
    
    Assert.That(mcWidget.Right, Is.InstanceOf<TimeLinesWidget>());
    Assert.That(UrnsOf(mcWidget.Right).First(), Is.EqualTo(urn2));
  }
  
  [Test]
  public void TimeLineChartAndStackedBarsChart()
  {
    var urn1 = Urn.BuildUrn("urn1");
    var urn2 = Urn.BuildUrn("urn2");
    
    var chart = Chart.Multi(
      Chart.StackedTimeBars(Of(urn1)),
      Chart.TimeLines(Of(urn2))
    ).Width(500).Height(300);

    var widget = chart.CreateWidget();

    Assert.That(widget.Width, Is.EqualTo(500));
    Assert.That(widget.Height, Is.EqualTo(300));

    Assert.That(widget, Is.InstanceOf<MultiChartWidget>());
    var mcWidget = (MultiChartWidget)widget;
    
    Assert.That(mcWidget.Left, Is.InstanceOf<StackedTimeBarsWidget>());
    Assert.That(UrnsOf(mcWidget.Left).First(), Is.EqualTo(urn1));
    
    Assert.That(mcWidget.Right, Is.InstanceOf<TimeLinesWidget>());
    Assert.That(UrnsOf(mcWidget.Right).First(), Is.EqualTo(urn2));
  }

  private IEnumerable<Urn> UrnsOf(ChartXTimeYWidget w) =>
    w.Content
      .Select(fd => fd.Value)
      .Cast<TimeSeriesFeed>()
      .Select(tsf => tsf.Urn);
}