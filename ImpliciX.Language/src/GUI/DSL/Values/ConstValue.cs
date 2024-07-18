namespace ImpliciX.Language.GUI
{
  public class ConstValue<T> : Value
  {
    private readonly T _value;
    public ConstValue(T value) => _value = value;
    public override Feed CreateFeed() => Const.Is(_value);
  }
}