namespace ImpliciX.Language.GUI
{
  public class DateTime : Block
  {
    public override Widget CreateWidget()
    {
      return new Text
      {
        Value = Feed,
        Style = Font?.CreateStyle(),
        Width = WidthValue
      };
    }

    internal NowFeed Feed { get; set; }
  }
}