using System.Drawing;
using System.Linq;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class TimeLinesTests : ScreensTests
{
  [Test]
  public void create_widget()
  {
    var timeSeriesUrn1 = Urn.BuildUrn("myMetric1");
    var timeSeriesUrn2 = Urn.BuildUrn("myMetric2");
        
    var chart = Chart.TimeLines(
      Of(timeSeriesUrn1).Fill(Color.Aqua),
      Of(timeSeriesUrn2)
    ).Width(200).Height(100);
        
    var widget = (TimeLinesWidget) chart.CreateWidget();

    Check.That(widget.Content).HasSize(2);
    Check.That(widget.Width).IsEqualTo(200);
    Check.That(widget.Height).IsEqualTo(100);
        
    var feedUrns = widget.Content
      .Select(it => it.Value).Cast<TimeSeriesFeed>()
      .Select(it => it.Urn);
        
    Check.That(feedUrns).ContainsExactly(timeSeriesUrn1, timeSeriesUrn2);

    var feedFillColors = widget.Content.Select(it => it.FillColor);
    Check.That(feedFillColors).ContainsExactly(Color.Aqua, null);
  }
}