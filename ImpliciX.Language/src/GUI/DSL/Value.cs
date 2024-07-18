using System;

#pragma warning disable CS0660, CS0661

namespace ImpliciX.Language.GUI
{
  public abstract class Value
  {
    public static BooleanExpressionValue operator < (Value a, float b)
      => new BooleanExpressionValue<LowerThan>(a, new ConstValue<float>(b));
    public static BooleanExpressionValue operator > (Value a, float b)
      => new BooleanExpressionValue<GreaterThan>(a, new ConstValue<float>(b));
    
    public static BooleanExpressionValue operator == (Value a, Enum b)
      => new BooleanExpressionValue<EqualTo>(a, new ConstValue<Enum>(b));
    public static BooleanExpressionValue operator != (Value a, Enum b)
      => new BooleanExpressionValue<NotEqualTo>(a, new ConstValue<Enum>(b));

    public abstract Feed CreateFeed();
  }
}