#nullable enable
using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI;

public abstract class ChartBaseWithYAxis<TChart>
  : ChartBase<TChart> where TChart : ChartBase<TChart>
{
  protected ChartBaseWithYAxis(DecoratedUrn[] data) : base(data)
  {
  }

  public TChart YMin<T>(PropertyUrn<T> yMin) => Chain(() => _yMin = new PropertyValue<T>(yMin));
  protected Feed? YMinFeed => _yMin?.CreateFeed();
  private Value? _yMin;

  public TChart YMax<T>(PropertyUrn<T> yMax) => Chain(() => _yMax = new PropertyValue<T>(yMax));
  protected Feed? YMaxFeed => _yMax?.CreateFeed();
  private Value? _yMax;

}