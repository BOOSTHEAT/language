using System.Linq;
using ImpliciX.Language.GUI;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.GUI
{

  public class LayoutTests : ScreensTests
  {
    [Test]
    public void CreateColumn()
    {
      var blocks = new[] { CreateBlock(), CreateBlock(), CreateBlock() };
      var stackBlock = Column.Layout(blocks);
      var widget = stackBlock.CreateWidget();
      var composite = GetAsInstanceOf<Composite>(widget);
      Assert.That(composite.Arrange, Is.EqualTo(Composite.ArrangeAs.Column));
      Assert.That(composite.Content, Is.EqualTo(blocks.Select(b => b.CreateWidget())));
    }

    [Test]
    public void CreateColumnWithSpacing()
    {
      var blocks = new[] { CreateBlock(), CreateBlock(), CreateBlock() };
      var stackBlock = Column.Spacing(12).Layout(blocks);
      var widget = stackBlock.CreateWidget();
      var composite = GetAsInstanceOf<Composite>(widget);
      Assert.That(composite.Arrange, Is.EqualTo(Composite.ArrangeAs.Column));
      Assert.That(composite.Spacing, Is.EqualTo(12));
      Assert.That(composite.Content, Is.EqualTo(blocks.Select(b => b.CreateWidget())));
    }

    [Test]
    public void CreateRow()
    {
      var blocks = new[] { CreateBlock(), CreateBlock(), CreateBlock() };
      var stackBlock = Row.Layout(blocks);
      var widget = stackBlock.CreateWidget();
      var composite = GetAsInstanceOf<Composite>(widget);
      Assert.That(composite.Arrange, Is.EqualTo(Composite.ArrangeAs.Row));
      Assert.That(composite.Content, Is.EqualTo(blocks.Select(b => b.CreateWidget())));
    }


    [Test]
    public void AlignBlocks()
    {
      var blocks = new[] { CreateBlock(), CreateBlock(), CreateBlock(), CreateBlock() };
      var canvasBlock = Canvas.Layout(
        At.Origin.Put(blocks[0]),
        At.Left(8).Top(12).Put(blocks[1]),
        At.Right(5).Bottom(4).Put(blocks[2]),
        At.HorizontalCenterOffset(2).VerticalCenterOffset(-4).Put(blocks[3])
      );
      var widget = canvasBlock.CreateWidget();
      var composite = GetAsInstanceOf<Composite>(widget);
      Assert.That(composite.Arrange, Is.EqualTo(Composite.ArrangeAs.XY));
      Assert.That(composite.Content.Count(), Is.EqualTo(blocks.Length));
      var positions =
        from item in composite.Content
        let position = (
          item,
          item.X.FromStart, item.X.ToEnd, item.X.CenterOffset,
          item.Y.FromStart, item.Y.ToEnd, item.Y.CenterOffset
        )
        select position;
      Assert.That(positions, Is.EqualTo(new (Widget, int?, int?, int?, int?, int?, int?)[]
      {
        (blocks[0].CreateWidget(), 0, null, null, 0, null, null),
        (blocks[1].CreateWidget(), 8, null, null, 12, null, null),
        (blocks[2].CreateWidget(), null, 5, null, null, 4, null),
        (blocks[3].CreateWidget(), null, null, 2, null, null, -4)
      }));
    }
  }
}