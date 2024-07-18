using System.Drawing;

namespace ImpliciX.Language.GUI
{
  public class Box : Block
  {
    public new Box Width(int pixels)
    {
      _width = pixels;
      return this;
    }

    public override Widget CreateWidget()
    {
      return new BoxWidget
      {
        Radius = _radius,
        Width = _width,
        Height = _height,
        Style = new Style { BackColor = _background, FrontColor = _border }
      };
    }

    public Box Height(int pixels)
    {
      _height = pixels;
      return this;
    }
    public Box Fill(Color color)
    {
      _background = color;
      return this;
    }
    public Block Border(Color color)
    {
      _border = color;
      return this;
    }
    public Box Radius(int pixels)
    {
      _radius = pixels;
      return this;
    }

    private int? _radius;
    private int? _width;
    private int? _height;
    private Color? _border;
    private Color? _background;

  }
}