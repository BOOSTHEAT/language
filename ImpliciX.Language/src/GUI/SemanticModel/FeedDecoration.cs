#nullable enable
using System.Drawing;

namespace ImpliciX.Language.GUI;

public struct FeedDecoration
{
  public Feed Value { get; }
  public Style? LabelStyle { get; private set; }
  public Color? FillColor { get; private set; }

  public FeedDecoration(Feed value, Style? labelStyle, Color? fillColor)
  {
    Value = value;
    LabelStyle = labelStyle;
    FillColor = fillColor;
  }
}