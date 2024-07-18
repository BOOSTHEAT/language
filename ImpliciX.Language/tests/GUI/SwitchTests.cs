using System.Linq;
using ImpliciX.Language.GUI;
using NUnit.Framework;
namespace ImpliciX.Language.Tests.GUI
{

  public class SwitchTests : ScreensTests
  {
    [Test]
    public void CreateSwitchWithDefaultCaseOnly()
    {
      var defaultBlock = CreateBlock();
      var switchBlock = Switch.Default(defaultBlock);
      var widget = switchBlock.CreateWidget();
      var switcher = GetAsInstanceOf<SwitchWidget>(widget);
      Assert.That(switcher.Default, Is.EqualTo(defaultBlock.CreateWidget()));
    }

    [Test]
    public void CreateSwitchWithSingleCase()
    {
      var case1Block = (CreateBooleanExpression(), CreateBlock());
      var defaultBlock = CreateBlock();
      var switchBlock = Switch
        .Case(case1Block.Item1, case1Block.Item2)
        .Default(defaultBlock);
      var widget = switchBlock.CreateWidget();
      var switcher = GetAsInstanceOf<SwitchWidget>(widget);
      Assert.That(switcher.Default, Is.EqualTo(defaultBlock.CreateWidget()));
      Assert.That(switcher.Cases.Count(), Is.EqualTo(1));
      var case1 = switcher.Cases.First();
      Assert.That(case1.When, Is.EqualTo(case1Block.Item1.CreateFeed()));
      Assert.That(case1.Then, Is.EqualTo(case1Block.Item2.CreateWidget()));
    }

    [Test]
    public void CreateSwitchWithMultipleCases()
    {
      var case1Block = (CreateBooleanExpression(), CreateBlock());
      var case2Block = (CreateBooleanExpression(), CreateBlock());
      var defaultBlock = CreateBlock();
      var switchBlock = Switch
        .Case(case1Block.Item1, case1Block.Item2)
        .Case(case2Block.Item1, case2Block.Item2)
        .Default(defaultBlock);
      var widget = switchBlock.CreateWidget();
      var switcher = GetAsInstanceOf<SwitchWidget>(widget);
      Assert.That(switcher.Default, Is.EqualTo(defaultBlock.CreateWidget()));
      Assert.That(switcher.Cases.Count(), Is.EqualTo(2));
      var case1 = switcher.Cases.First();
      Assert.That(case1.When, Is.EqualTo(case1Block.Item1.CreateFeed()));
      Assert.That(case1.Then, Is.EqualTo(case1Block.Item2.CreateWidget()));
      var case2 = switcher.Cases.Last();
      Assert.That(case2.When, Is.EqualTo(case2Block.Item1.CreateFeed()));
      Assert.That(case2.Then, Is.EqualTo(case2Block.Item2.CreateWidget()));
      Assert.That(case1.When, Is.Not.EqualTo(case2.When));
    }
  }
}
