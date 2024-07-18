using System;

namespace ImpliciX.Language.GUI;

public abstract class ChartBase<T> : FixedSizeBlock<T>
  where T: ChartBase<T>
{
  internal DecoratedUrn[] Data { get; }
  protected abstract T This { get; }

  protected ChartBase(DecoratedUrn[] data)
  {
    Data = data ?? throw new ArgumentNullException(nameof(data));
  }

  protected T Chain(Action a)
  {
    a();
    return This;
  }
}