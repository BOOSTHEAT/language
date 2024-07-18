using System;

namespace ImpliciX.Language.GUI;

public abstract class FixedSizeBlock<T> : Block
  where T: FixedSizeBlock<T>
{
  protected int? HeightValue { get; private set; }

  public T Height(int pixels)
  {
    HeightValue = pixels;
    return (T)this;
  }

  public new T Width(int pixels)
  {
    WidthValue = pixels;
    return (T)this;
  }
}