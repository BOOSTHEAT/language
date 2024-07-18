using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed class PieChart : ChartBase<PieChart>
{
  public PieChart(DecoratedUrn[] slices) : base(slices)
  {
  }

  public override Widget CreateWidget() => new PieChartWidget
  {
    Content = Data
      .Select(slice => slice.ToFeedDecoration())
      .ToArray(),
    Height = HeightValue,
    Width = WidthValue
  };

  protected override PieChart This => this;
}