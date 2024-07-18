using System.Collections.Generic;
using System.Linq;

namespace ImpliciX.Language.GUI
{
  public class Switch : Block
  {
    public class Partial
    {
      public Partial Case(BooleanExpressionValue condition, Block block)
      {
        _cases.Add(new CaseBlock(condition,block));
        return this;
      } 
      public Switch Default(Block block) => new (this, block);
      internal readonly List<CaseBlock> _cases = new ();
    }
    
    private Block _defaultBlock;
    private Partial _partial;

    private Switch(Partial partial, Block defaultBlock)
    {
      _partial = partial;
      _defaultBlock = defaultBlock;
    }


    public override Widget CreateWidget() =>
      new SwitchWidget
      {
        Cases = _partial._cases.Select(c => c.CreateWhenThen()).ToArray(),
        Default = _defaultBlock.CreateWidget()
      };

    internal class CaseBlock
    {
      private readonly BooleanExpressionValue _condition;
      private readonly Block _block;

      public CaseBlock(BooleanExpressionValue condition, Block block)
      {
        _condition = condition;
        _block = block;
      }

      public SwitchWidget.Case CreateWhenThen()
      {
        return new SwitchWidget.Case
        {
          When = (BinaryExpression)_condition.CreateFeed(),
          Then = _block.CreateWidget()
        };
      }
    }
  }
}