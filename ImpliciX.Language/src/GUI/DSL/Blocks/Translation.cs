namespace ImpliciX.Language.GUI
{
  public class Translation : Block
  {
    public override Widget CreateWidget()
    {
      return new Text
      {
        Value = Const.IsTranslate(Text),
        Style = Font?.CreateStyle(),
        Width = WidthValue
      };
    }

    internal string Text { get; set; }
  }
}