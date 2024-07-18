using System.Drawing;
using System.Linq;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI;

public class BlockTests : ScreensTests
{
  [Test]
  public void CreateLabel()
  {
    var labelBlock = Label("foo").Width(28);
    var widget = labelBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<Const<string>>(text.Value);
    Assert.That(text.Width, Is.EqualTo(28));
    Assert.That(feed.Value, Is.EqualTo("foo"));
    Assert.That(feed.Translate, Is.False);
  }

  [Test]
  public void CreateTranslation()
  {
    var translationBlock = Translate("foo").Width(28);
    var widget = translationBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<Const<string>>(text.Value);
    Assert.That(text.Width, Is.EqualTo(28));
    Assert.That(feed.Value, Is.EqualTo("foo"));
    Assert.That(feed.Translate, Is.True);
  }

  [Test]
  public void CreateMeasure()
  {
    var measureBlock = Show(CreateMeasure("foo:bar")).Width(28);
    var widget = measureBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<MeasureFeed>(text.Value);
    Assert.That(text.Width, Is.EqualTo(28));
    Assert.That(feed.Urn.Value, Is.EqualTo("foo:bar"));
  }

  [Test]
  public void CreateProperty()
  {
    var propertyBlock = Show(CreateProperty("foo:bar")).Width(28);
    var widget = propertyBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<PropertyFeed>(text.Value);
    Assert.That(text.Width, Is.EqualTo(28));
    Assert.That(feed.Urn.Value, Is.EqualTo("foo:bar"));
  }

  [Test]
  public void CreateDate()
  {
    var dateTimeBlock = Now.Date;
    var widget = dateTimeBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<NowFeed>(text.Value);
    Assert.That(feed, Is.EqualTo(NowFeed.Date));
  }

  [Test]
  public void CreateWeekDay()
  {
    var dateTimeBlock = Now.WeekDay;
    var widget = dateTimeBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<NowFeed>(text.Value);
    Assert.That(feed, Is.EqualTo(NowFeed.WeekDay));
  }

  [Test]
  public void CreateHoursMinutesSeconds()
  {
    var dateTimeBlock = Now.HoursMinutesSeconds;
    var widget = dateTimeBlock.CreateWidget();
    var text = GetAsInstanceOf<Text>(widget);
    var feed = GetAsInstanceOf<NowFeed>(text.Value);
    Assert.That(feed, Is.EqualTo(NowFeed.HoursMinutesSeconds));
  }

  [Test]
  public void CreateImage()
  {
    var imageBlock = Image("some/path");
    var widget = imageBlock.CreateWidget();
    var image = GetAsInstanceOf<ImageWidget>(widget);
    Assert.That(image.IsBase, Is.False);
    var feed = GetAsInstanceOf<Const<string>>(image.Path);
    Assert.That(feed.Value, Is.EqualTo("some/path"));
  }

  [Test]
  public void CreateBackgroundImage()
  {
    var imageBlock = Background(Image("some/path")).Layout();
    var widget = imageBlock.CreateWidget();
    var canvas = GetAsInstanceOf<Composite>(widget);
    Assert.That(canvas.Content.Count(), Is.EqualTo(1));
    var image = GetAsInstanceOf<ImageWidget>(canvas.Content.First());
    Assert.That(image.IsBase, Is.True);
    var feed = GetAsInstanceOf<Const<string>>(image.Path);
    Assert.That(feed.Value, Is.EqualTo("some/path"));
  }

  [Test]
  public void CreateDataDrivenImageWithMeasure()
  {
    var imageBlock = Image("some/path").DataDriven(CreateMeasure("foo:bar"), 1.2, 3.4);
    var widget = imageBlock.CreateWidget();
    var image = GetAsInstanceOf<DataDrivenImageWidget>(widget);
    Assert.That(image.IsBase, Is.False);
    var imageFeed = GetAsInstanceOf<Const<string>>(image.Path);
    Assert.That(imageFeed.Value, Is.EqualTo("some/path"));
    var valueFeed = GetAsInstanceOf<MeasureFeed>(image.Value);
    Assert.That(valueFeed.Urn.Value, Is.EqualTo("foo:bar"));
    var floorFeed = GetAsInstanceOf<Const<double>>(image.Floor);
    Assert.That(floorFeed.Value, Is.EqualTo(1.2));
    var stepFeed = GetAsInstanceOf<Const<double>>(image.Step);
    Assert.That(stepFeed.Value, Is.EqualTo(3.4));
  }

  [Test]
  public void CreateDataDrivenImageWithProperty()
  {
    var imageBlock = Image("some/path").DataDriven(CreateProperty("foo:bar"), 1.2, 3.4);
    var widget = imageBlock.CreateWidget();
    var image = GetAsInstanceOf<DataDrivenImageWidget>(widget);
    Assert.That(image.IsBase, Is.False);
    var imageFeed = GetAsInstanceOf<Const<string>>(image.Path);
    Assert.That(imageFeed.Value, Is.EqualTo("some/path"));
    var valueFeed = GetAsInstanceOf<PropertyFeed>(image.Value);
    Assert.That(valueFeed.Urn.Value, Is.EqualTo("foo:bar"));
    var floorFeed = GetAsInstanceOf<Const<double>>(image.Floor);
    Assert.That(floorFeed.Value, Is.EqualTo(1.2));
    var stepFeed = GetAsInstanceOf<Const<double>>(image.Step);
    Assert.That(stepFeed.Value, Is.EqualTo(3.4));
  }

  [Test]
  public void CreateBox()
  {
    var boxBlock = Box.Width(84).Height(480).Radius(12)
      .Fill(Color.FromArgb(151, 151, 151))
      .Border(Color.FromArgb(51, 51, 51));

    var widget = boxBlock.CreateWidget();
    var box = GetAsInstanceOf<BoxWidget>(widget);
    Assert.That(box.Radius, Is.EqualTo(12));
    Assert.That(box.Width, Is.EqualTo(84));
    Assert.That(box.Height, Is.EqualTo(480));
    Assert.That(box.Style.FrontColor, Is.EqualTo(Color.FromArgb(51, 51, 51)));
    Assert.That(box.Style.BackColor, Is.EqualTo(Color.FromArgb(151, 151, 151)));
  }

  [Test]
  public void CreateBackgroundBox()
  {
    var boxBlock = Box.Width(84).Height(480).Radius(12)
      .Fill(Color.FromArgb(151, 151, 151))
      .Border(Color.FromArgb(51, 51, 51));

    var backgroundBoxBlock = Background(boxBlock).Layout();
    var widget = backgroundBoxBlock.CreateWidget();
    var canvas = GetAsInstanceOf<Composite>(widget);
    Assert.That(canvas.Content.Count(), Is.EqualTo(1));
    var box = GetAsInstanceOf<BoxWidget>(canvas.Content.First());
    Assert.That(box.IsBase, Is.True);
    Assert.That(box.Radius, Is.EqualTo(12));
    Assert.That(box.Width, Is.EqualTo(84));
    Assert.That(box.Height, Is.EqualTo(480));
    Assert.That(box.Style.FrontColor, Is.EqualTo(Color.FromArgb(51, 51, 51)));
    Assert.That(box.Style.BackColor, Is.EqualTo(Color.FromArgb(151, 151, 151)));
  }

  [Test]
  public void WhenIAddIncrementationForPropertyUrnTWhereTIsIFloat()
  {
    var inputProperty = PropertyUrn<Temperature>.Build("production", "heating", "temperature_threshold");
    const double incrementValue = 1.0;
    var block = CreateBlock();
    var increment = block.Increment(inputProperty, incrementValue);

    Assert.That(increment is Increment<Temperature>, Is.True);
    var sut = (IncrementWidget) increment.CreateWidget();

    var blockWidget = block.CreateWidget();
    Assert.That(sut.Visual, Is.EqualTo(blockWidget));
    Assert.That(GetAsInstanceOf<PropertyFeed>(sut.InputUrn).Urn, Is.EqualTo(Urn.BuildUrn(inputProperty)));
    Assert.That(sut.StepValue, Is.EqualTo(incrementValue));
  }

  [Test]
  public void WhenIAddIncrementationForUserSetting()
  {
    var inputProperty = PropertyUrn<IFloat>.Build("system", "settings", "temperature_threshold");
    const double incrementValue = -4.0;
    var block = CreateBlock();
    var increment = block.Increment(inputProperty, incrementValue);

    Assert.That(increment is Increment<IFloat>, Is.True);
    var sut = (IncrementWidget) increment.CreateWidget();

    var blockWidget = block.CreateWidget();
    Assert.That(sut.Visual, Is.EqualTo(blockWidget));
    Assert.That(GetAsInstanceOf<PropertyFeed>(sut.InputUrn).Urn, Is.EqualTo(Urn.BuildUrn(inputProperty)));
    Assert.That(sut.StepValue, Is.EqualTo(incrementValue));
  }

  [Test]
  public void WhenISend()
  {
    var block = CreateBlock();
    var commandUrn = CommandUrn<NoArg>.Build(nameof(BlockTests).ToLower(), "my_command");
    var send = block.Send(commandUrn);

    Assert.That(send is Send, Is.True);
    var sut = (SendWidget) send.CreateWidget();

    var blockWidget = block.CreateWidget();
    Assert.That(sut.Visual, Is.EqualTo(blockWidget));
    Assert.That(sut.CommandUrn, Is.EqualTo(commandUrn));
  }
}