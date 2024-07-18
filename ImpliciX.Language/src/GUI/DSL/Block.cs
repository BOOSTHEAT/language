using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public abstract class Block
{
  public Block With(Font font)
  {
    Font = font;
    return this;
  }

  public Block Width(int pixels)
  {
    WidthValue = pixels;
    return this;
  }

  public Block NavigateTo(GuiNode target, Block selectionMark) => new Navigator(target, this, selectionMark);

  public abstract Widget CreateWidget();
  internal Font Font { get; private set; }
  protected int? WidthValue { get; set; }

  public Block Increment<T>(PropertyUrn<T> inputProperty, double incrementValue) where T : IFloat
    => new Increment<T>(this, inputProperty, incrementValue);

  public Block Send(CommandUrn<NoArg> commandUrn) => new Send(this, commandUrn);
}