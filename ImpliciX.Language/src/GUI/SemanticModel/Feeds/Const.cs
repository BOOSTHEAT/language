namespace ImpliciX.Language.GUI
{
  
  public class Const : Feed
  {
    public static Const<T> Is<T>(T value) => new Const<T>(value);
    public static Const<T> IsTranslate<T>(T value) => new Const<T>(value) { Translate = true };
  }
  
  public class Const<T> : Const
  {
    public readonly T Value;

    public Const(T value)
    {
      Value = value;
    }
  }
  
}