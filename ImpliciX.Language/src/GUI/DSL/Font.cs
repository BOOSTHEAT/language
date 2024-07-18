using System.Drawing;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI
{
  public record Font
  {
    public Font ExtraBold
    {
      get
      {
        _family = Style.Family.ExtraBold;
        return this;
      }
    }

    public Font Medium
    {
      get
      {
        _family = Style.Family.Medium;
        return this;
      }
    }

    public Font Light
    {
      get
      {
        _family = Style.Family.Light;
        return this;
      }
    }

    public Font Size(int size)
    {
      _fontSize = size;
      return this;
    }

    public Font Color(Color color)
    {
      _fontColor = color;
      return this;
    }

    public Style CreateStyle()
    {
      return new Style {FontSize = _fontSize, FrontColor = _fontColor, FontFamily = _family};
    }

    private int _fontSize;
    private Color _fontColor;
    private Style.Family _family;
  }
}