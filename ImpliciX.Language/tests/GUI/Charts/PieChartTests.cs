using System.Drawing;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class PieChartTests : ScreensTests
{
  [Test]
  public void GivenIWritePieChart_WhenPieChartIsCreated_ThenPieChartContainsDataExpected()
  {
    var metricUrnIndex0 = MetricUrn.Build("myMetric1");
    var metricUrnIndex1 = MetricUrn.Build("myMetric2");

    var fontIndex0 = Font.ExtraBold.Size(14).Color(Color.Black);
    var fontIndex1 = Font.ExtraBold.Size(22).Color(Color.Red);

    var pieChart = Chart.Pie(
      Of(metricUrnIndex0)
        .Fill(Color.Aqua)
        .With(fontIndex0),
      Of(metricUrnIndex1)
        .Fill(Color.Orange)
        .With(fontIndex1)
    );

    var slices = pieChart.Data;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsEqualTo(Color.Aqua);
    Check.That(slices[0].Urn).IsEqualTo(metricUrnIndex0);
    Check.That(slices[0].LabelFont!.Equals(fontIndex0)).IsTrue();

    Check.That(slices[1].FillColor).IsEqualTo(Color.Orange);
    Check.That(slices[1].Urn).IsEqualTo(metricUrnIndex1);
    Check.That(slices[1].LabelFont!.Equals(fontIndex1)).IsTrue();
  }

  [Test]
  public void GivenIWritePieChart_WhenICreateWidget_ThenWidgetContainsDataExpected()
  {
    var metricUrnIndex0 = MetricUrn.Build("myMetric1");
    var metricUrnIndex1 = MetricUrn.Build("myMetric2");

    var fontIndex0 = Font.ExtraBold.Size(14).Color(Color.Black);
    var fontIndex1 = Font.ExtraBold.Size(22).Color(Color.Red);

    var pieChart = Chart.Pie(
      Of(metricUrnIndex0)
        .Fill(Color.Aqua)
        .With(fontIndex0),
      Of(metricUrnIndex1)
        .Fill(Color.Orange)
        .With(fontIndex1)
    );

    var widget = (PieChartWidget) pieChart.CreateWidget();

    var slices = widget.Content;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsEqualTo(Color.Aqua);
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[0].Value).Urn).IsEqualTo(metricUrnIndex0);
    Check.That(slices[0].LabelStyle!.Equals(fontIndex0.CreateStyle())).IsTrue();

    Check.That(slices[1].FillColor).IsEqualTo(Color.Orange);
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[1].Value).Urn).IsEqualTo(metricUrnIndex1);
    Check.That(slices[1].LabelStyle!.Equals(fontIndex1.CreateStyle())).IsTrue();
  }

  [Test]
  public void GivenIWritePieChartWithMiscPropertyTypes_WhenICreateWidget_ThenWidgetContainsDataExpected()
  {
    var urnIndex0 = PropertyUrn<Temperature>.Build("myValue1");
    var urnIndex1 = PropertyUrn<Pressure>.Build("myValue2");

    var fontIndex0 = Font.ExtraBold.Size(14).Color(Color.Black);
    var fontIndex1 = Font.ExtraBold.Size(22).Color(Color.Red);

    var pieChart = Chart.Pie(
      Of(urnIndex0)
        .Fill(Color.Aqua)
        .With(fontIndex0),
      Of(urnIndex1)
        .Fill(Color.Orange)
        .With(fontIndex1)
    );

    var widget = (PieChartWidget) pieChart.CreateWidget();

    var slices = widget.Content;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsEqualTo(Color.Aqua);
    Check.That(GetAsInstanceOf<PropertyFeed<Temperature>>(slices[0].Value).Urn).IsEqualTo(urnIndex0);
    Check.That(slices[0].LabelStyle!.Equals(fontIndex0.CreateStyle())).IsTrue();

    Check.That(slices[1].FillColor).IsEqualTo(Color.Orange);
    Check.That(GetAsInstanceOf<PropertyFeed<Pressure>>(slices[1].Value).Urn).IsEqualTo(urnIndex1);
    Check.That(slices[1].LabelStyle!.Equals(fontIndex1.CreateStyle())).IsTrue();
  }

  [Test]
  public void GivenIWritePieChartWithoutStyle_WhenICreateWidget_ThenWidgetContainsDataExpected()
  {
    var metricUrnIndex0 = MetricUrn.Build("myMetric1");
    var metricUrnIndex1 = MetricUrn.Build("myMetric2");

    var pieChart = Chart.Pie(
      Of(metricUrnIndex0),
      Of(metricUrnIndex1)
    );

    var widget = (PieChartWidget) pieChart.CreateWidget();

    var slices = widget.Content;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsNull();
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[0].Value).Urn).IsEqualTo(metricUrnIndex0);
    Check.That(slices[0].LabelStyle).IsNull();

    Check.That(slices[1].FillColor).IsNull();
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[1].Value).Urn).IsEqualTo(metricUrnIndex1);
    Check.That(slices[1].LabelStyle).IsNull();
  }

  [Test]
  public void GivenIWritePieChartOnlyWithFillColor_WhenICreateWidget_ThenWidgetContainsDataExpected()
  {
    var metricUrnIndex0 = MetricUrn.Build("myMetric1");
    var metricUrnIndex1 = MetricUrn.Build("myMetric2");

    var pieChart = Chart.Pie(
      Of(metricUrnIndex0)
        .Fill(Color.Aqua),
      Of(metricUrnIndex1)
        .Fill(Color.Orange)
    );

    var widget = (PieChartWidget) pieChart.CreateWidget();

    var slices = widget.Content;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsEqualTo(Color.Aqua);
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[0].Value).Urn).IsEqualTo(metricUrnIndex0);
    Check.That(slices[0].LabelStyle).IsNull();

    Check.That(slices[1].FillColor).IsEqualTo(Color.Orange);
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[1].Value).Urn).IsEqualTo(metricUrnIndex1);
    Check.That(slices[1].LabelStyle).IsNull();
  }

  [Test]
  public void GivenIWritePieChartOnlyWithLabelFont_WhenICreateWidget_ThenWidgetContainsDataExpected()
  {
    var metricUrnIndex0 = MetricUrn.Build("myMetric1");
    var metricUrnIndex1 = MetricUrn.Build("myMetric2");

    var fontIndex0 = Font.ExtraBold.Size(14).Color(Color.Black);
    var fontIndex1 = Font.ExtraBold.Size(22).Color(Color.Red);

    var pieChart = Chart.Pie(
      Of(metricUrnIndex0)
        .With(fontIndex0),
      Of(metricUrnIndex1)
        .With(fontIndex1)
    );

    var widget = (PieChartWidget) pieChart.CreateWidget();

    var slices = widget.Content;
    Check.That(slices).HasSize(2);

    Check.That(slices[0].FillColor).IsNull();
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[0].Value).Urn).IsEqualTo(metricUrnIndex0);
    Check.That(slices[0].LabelStyle!.Equals(fontIndex0.CreateStyle())).IsTrue();

    Check.That(slices[1].FillColor).IsNull();
    Check.That(GetAsInstanceOf<PropertyFeed<MetricValue>>(slices[1].Value).Urn).IsEqualTo(metricUrnIndex1);
    Check.That(slices[1].LabelStyle!.Equals(fontIndex1.CreateStyle())).IsTrue();
  }

  [TestCase(null, null)]
  [TestCase(null, 300)]
  [TestCase(200, null)]
  [TestCase(200, 300)]
  public void GivenIWritePieChartWithHeightAndWidth_WhenICreateWidget_ThenWidgetContainsDataExpected(int? height, int? width)
  {
    var pieChart = Chart.Pie(
      Of(MetricUrn.Build("myMetric1")),
      Of(MetricUrn.Build("myMetric2"))
    );

    if (height.HasValue)
      pieChart = pieChart.Height(height.Value);

    if (width.HasValue)
      pieChart = pieChart.Width(width.Value);

    var widget = (PieChartWidget) pieChart.CreateWidget();

    Check.That(widget.Height).IsEqualTo(height);
    Check.That(widget.Width).IsEqualTo(width);
  }
}