using System;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI
{

  public class ScreensTests : Screens
  {

    protected T GetAsInstanceOf<T>(object obj) where T : class
    {
      Assert.That(obj, Is.InstanceOf<T>());
      return (obj as T)!;
    }

    protected MeasureNode<IFloat> CreateMeasure(string name)
      => CreateNode(name, (r, n) => new MeasureNode<IFloat>(n, r));

    protected GuiNode CreateGuiNode(string name) => CreateNode(name, (r, n) => new GuiNode(r, n));

    private static T CreateNode<T>(string name, Func<RootModelNode, string, T> factory)
      where T : ModelNode
    {
      var names = name.Split(':');
      return factory(new RootModelNode(names[0]), names[1]);
    }

    public enum Something
    {
      Foo,
      Bar
    }

    protected PropertyUrn<Something> CreateProperty(string name)
      => PropertyUrn<Something>.Build(Urn.Deconstruct(name));
    protected PropertyUrn<T> CreateProperty<T>(string name)
      => PropertyUrn<T>.Build(Urn.Deconstruct(name));

    protected AlignedBlock CreateAlignedBlock() => At.Origin.Put(CreateBlock());
    protected Block CreateBlock() => new BlockStub();

    class BlockStub : Block
    {
      private readonly Widget _widget;
      public BlockStub() => _widget = new WidgetStub();
      public override Widget CreateWidget() => _widget;
    }

    class WidgetStub : Widget
    {
    }


    protected BooleanExpressionValue CreateBooleanExpression() => new BooleanExpressionStub();

    public class BooleanExpressionStub : BooleanExpressionValue
    {
      private readonly BinaryExpression _feed;
      public BooleanExpressionStub() : base(null, null) => _feed = new FeedStub();
      public override Feed CreateFeed() => _feed;
    }

    class FeedStub : BinaryExpression
    {
    }
  }
}
