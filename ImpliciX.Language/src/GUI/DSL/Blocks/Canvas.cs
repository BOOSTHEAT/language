using System.Collections.Generic;
using System.Linq;

namespace ImpliciX.Language.GUI
{
  public class Canvas : Block
  {
    public class Empty
    {
      internal Block Background { get; set; }

      public Block Layout(params AlignedBlock[] content) => new Canvas { Background = Background, _content = content };
    }
    
    internal Block Background { get; set; }
    private AlignedBlock[] _content;

    public override Widget CreateWidget() => new Composite { Content = GetContent().ToArray() };

    private IEnumerable<Widget> GetContent()
    {
      if (Background != null)
      {
        var background = Background.CreateWidget();
        background.IsBase = true;
        yield return background;
      }
      if(_content == null)
        yield break;
      foreach (var aligned in _content)
      {
        var widget = aligned.CreateWidget();
        yield return widget;
      }
    }

  }
}