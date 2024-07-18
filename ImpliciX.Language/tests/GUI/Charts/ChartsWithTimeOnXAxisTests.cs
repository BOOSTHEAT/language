using ImpliciX.Language.GUI;
using ImpliciX.Language.Metrics.Internals;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI.Charts;

public class ChartsWithTimeOnXAxisTests : ScreensTests
{
  [Test]
  public void OverThePastForTimeLines()
  {
    var foo = CreateProperty<Temperature>("foo");
    var sut = Chart
      .TimeLines(Of(foo))
      .Over.ThePast(6).Months;
    var widget = (TimeLinesWidget) sut.CreateWidget();
    Assert.That(widget.XSpan, Is.EqualTo(new ChartXTimeSpan(6, TimeUnit.Months)));
  }

  [Test]
  public void OverThePastForTimeBars()
  {
    var foo = CreateProperty<Temperature>("foo");
    var sut = Chart
      .StackedTimeBars(Of(foo))
      .Over.ThePast(6).Months;
    var widget = (StackedTimeBarsWidget) sut.CreateWidget();
    Assert.That(widget.XSpan, Is.EqualTo(new ChartXTimeSpan(6, TimeUnit.Months)));
  }
}