// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public class Widget
{
  public SizeAndPosition X { get; } = new ();
  public SizeAndPosition Y { get; } = new ();

  public int? Left
  {
    get => X.FromStart;
    set => X.FromStart = value;
  }

  public int? Top
  {
    get => Y.FromStart;
    set => Y.FromStart = value;
  }

  public int? Right
  {
    get => X.ToEnd;
    set => X.ToEnd = value;
  }

  public int? Bottom
  {
    get => Y.ToEnd;
    set => Y.ToEnd = value;
  }

  public int? Width
  {
    get => X.Size;
    set => X.Size = value;
  }

  public int? Height
  {
    get => Y.Size;
    set => Y.Size = value;
  }

  public int? HorizontalCenterOffset
  {
    get => X.CenterOffset;
    set => X.CenterOffset = value;
  }

  public int? VerticalCenterOffset
  {
    get => Y.CenterOffset;
    set => Y.CenterOffset = value;
  }

  public Style Style;
  public bool IsBase { get; set; }
}

public class SizeAndPosition
{
  public int? Size;
  public int? FromStart;
  public int? ToEnd;
  public int? CenterOffset;
}