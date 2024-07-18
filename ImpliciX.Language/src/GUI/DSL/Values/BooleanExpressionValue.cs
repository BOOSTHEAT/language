using System;

namespace ImpliciX.Language.GUI
{
  public abstract class BooleanExpressionValue : Value
  {
    protected readonly Value Left;
    protected readonly Value Right;

    public BooleanExpressionValue(Value left, Value right)
    {
      Left = left;
      Right = right;
    }
  }
  public class BooleanExpressionValue<T> : BooleanExpressionValue
    where T : BinaryExpression
  {
    public BooleanExpressionValue(Value left, Value right) : base(left,right)
    {
    }

    public override Feed CreateFeed()
    {
      var feed = Activator.CreateInstance<T>();
      feed.Left = Left.CreateFeed();
      feed.Right = Right.CreateFeed();
      return feed;
    }

  }
}