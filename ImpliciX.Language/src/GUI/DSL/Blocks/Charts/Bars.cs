#nullable enable

using System.Linq;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public class Bars : ChartBaseWithYAxis<Bars>
{
  internal Bars(DecoratedUrn[] data) : base(data)
  {
  }

  public override Widget CreateWidget() => new BarsWidget
  {
    Width = WidthValue,
    Height = HeightValue,
    Content = Data
      .Select(data => data.ToFeedDecoration())
      .ToArray(),
    YMin = YMinFeed,
    YMax = YMaxFeed
  };

  protected override Bars This => this;
}