namespace ImpliciX.Language.GUI
{
  public class Label : Block
  {
    public override Widget CreateWidget()
    {
      return new Text
      {
        Value = Const.Is(Text),
        Style = Font?.CreateStyle(),
        Width = WidthValue
      };
    }

    internal string Text { get; set; }
  }
}