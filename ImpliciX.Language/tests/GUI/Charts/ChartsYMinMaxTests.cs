using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class ChartsYMinMaxTests : ScreensTests
{
  [Test]
  public void GivenTimeLines_WhenISetYMin()
  {
    var metricUrn1 = MetricUrn.Build("myMetric1");

    var yMin = CreateProperty<Flow>("chart_ymin");
    var chart = Chart.TimeLines(Of(metricUrn1)).YMin(yMin);

    var widget = (TimeLinesWidget) chart.CreateWidget();
    Check.That(widget.YMin).IsNotNull();
    Check.That(widget.YMax).IsNull();
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMin).Urn).IsEqualTo(yMin);
  }

  [Test]
  public void GivenTimeLines_WhenISetYMax()
  {
    var metricUrn1 = MetricUrn.Build("myMetric1");

    var yMax = CreateProperty<Flow>("chart_ymax");
    var chart = Chart.TimeLines(Of(metricUrn1)).YMax(yMax);

    var widget = (TimeLinesWidget) chart.CreateWidget();
    Check.That(widget.YMin).IsNull();
    Check.That(widget.YMax).IsNotNull();
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMax).Urn).IsEqualTo(yMax);
  }

  [Test]
  public void GivenTimeLines_WhenISetYMinAndYMax()
  {
    var metricUrn1 = MetricUrn.Build("myMetric1");

    var yMin = CreateProperty<Flow>("chart_ymin");
    var yMax = CreateProperty<Flow>("chart_ymax");
    var chart = Chart.TimeLines(Of(metricUrn1))
      .YMin(yMin)
      .YMax(yMax);

    var widget = (TimeLinesWidget) chart.CreateWidget();
    Check.That(widget.YMin).IsNotNull();
    Check.That(widget.YMax).IsNotNull();
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMin).Urn).IsEqualTo(yMin);
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMax).Urn).IsEqualTo(yMax);
  }

  [Test]
  public void GivenBars_WhenISetYMinAndYMax()
  {
    var metricUrn1 = MetricUrn.Build("myMetric1");

    var yMin = CreateProperty<Flow>("chart_ymin");
    var yMax = CreateProperty<Flow>("chart_ymax");
    var chart = Chart.Bars(Of(metricUrn1))
      .YMin(yMin)
      .YMax(yMax);

    var widget = (BarsWidget) chart.CreateWidget();
    Check.That(widget.YMin).IsNotNull();
    Check.That(widget.YMax).IsNotNull();
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMin).Urn).IsEqualTo(yMin);
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMax).Urn).IsEqualTo(yMax);
  }

  [Test]
  public void GivenStackedTimeBars_WhenISetYMinAndYMax()
  {
    var metricUrn1 = MetricUrn.Build("myMetric1");

    var yMin = CreateProperty<Flow>("chart_ymin");
    var yMax = CreateProperty<Flow>("chart_ymax");
    var chart = Chart.StackedTimeBars(Of(metricUrn1))
      .YMin(yMin)
      .YMax(yMax);

    var widget = (StackedTimeBarsWidget) chart.CreateWidget();
    Check.That(widget.YMin).IsNotNull();
    Check.That(widget.YMax).IsNotNull();
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMin).Urn).IsEqualTo(yMin);
    Check.That(GetAsInstanceOf<PropertyFeed<Flow>>(widget.YMax).Urn).IsEqualTo(yMax);
  }
}