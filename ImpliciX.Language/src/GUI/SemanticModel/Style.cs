using System;
using System.Drawing;
namespace ImpliciX.Language.GUI
{
  public class Style : IEquatable<Style>
  {
    public Color? FrontColor;
    public Color? BackColor;
    public int? FontSize;
    public Family? FontFamily;
    
    public Style Override(Color? frontColor = default, Color? backColor = default, int? size = default, Family? family = default) =>
      new Style
      {
        FrontColor = frontColor ?? FrontColor,
        BackColor = backColor ?? BackColor,
        FontSize = size ?? FontSize,
        FontFamily = family ?? FontFamily
      };

    public enum Family
    {
      Light,
      Regular,
      Medium,
      Heavy,
      ExtraBold
    }

    public bool Equals(Style other)
    {
      if(other == null)
        return false;
      return FontSize == other.FontSize
             && FrontColor == other.FrontColor
             && BackColor == other.BackColor
             && FontFamily == other.FontFamily;
    }
  }
}