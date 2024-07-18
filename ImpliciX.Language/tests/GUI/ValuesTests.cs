using System;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI
{

  public class ValuesTests : ScreensTests
  {
    [Test]
    public void MeasureLowerThanConstant()
    {
      var expr = Value(CreateMeasure("x:y")) < 0.95f;
      var be = GetAsInstanceOf<LowerThan>(expr.CreateFeed());
      var left = GetAsInstanceOf<MeasureFeed<IFloat>>(be.Left);
      Assert.That(left.Urn.Value, Is.EqualTo("x:y"));
      var right = GetAsInstanceOf<Const<float>>(be.Right);
      Assert.That(right.Value, Is.EqualTo(0.95f));
    }

    [Test]
    public void MeasureGreaterThanConstant()
    {
      var expr = Value(CreateMeasure("x:y")) > 0.95f;
      var be = GetAsInstanceOf<GreaterThan>(expr.CreateFeed());
      var left = GetAsInstanceOf<MeasureFeed<IFloat>>(be.Left);
      Assert.That(left.Urn.Value, Is.EqualTo("x:y"));
      var right = GetAsInstanceOf<Const<float>>(be.Right);
      Assert.That(right.Value, Is.EqualTo(0.95f));
    }

    [Test]
    public void PropertyEqualToConstant()
    {
      var expr = Value(CreateProperty("x:y")) == Something.Foo;
      var be = GetAsInstanceOf<EqualTo>(expr.CreateFeed());
      var left = GetAsInstanceOf<PropertyFeed<Something>>(be.Left);
      Assert.That(left.Urn.Value, Is.EqualTo("x:y"));
      var right = GetAsInstanceOf<Const<Enum>>(be.Right);
      Assert.That(right.Value, Is.EqualTo(Something.Foo));
    }

    [Test]
    public void PropertyNotEqualToConstant()
    {
      var expr = Value(CreateProperty("x:y")) != Something.Foo;
      var be = GetAsInstanceOf<NotEqualTo>(expr.CreateFeed());
      var left = GetAsInstanceOf<PropertyFeed<Something>>(be.Left);
      Assert.That(left.Urn.Value, Is.EqualTo("x:y"));
      var right = GetAsInstanceOf<Const<Enum>>(be.Right);
      Assert.That(right.Value, Is.EqualTo(Something.Foo));
    }
  }
}