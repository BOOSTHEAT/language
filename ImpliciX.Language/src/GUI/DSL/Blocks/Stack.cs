using System.Linq;

namespace ImpliciX.Language.GUI
{
  public class Stack : Block
  {
    public class Empty
    {
      internal Composite.ArrangeAs ArrangeAs { get; set; }
      internal int? Space { get; set; }
      
      public Empty Spacing(int pixels)
      {
        Space = pixels;
        return this;
      }
      
      public Block Layout(params Block[] content)
      {
        return new Stack { _empty = this, Content = content };
      }
    }
    
    public override Widget CreateWidget()
    {
      return new Composite
      {
        Arrange = _empty.ArrangeAs,
        Spacing = _empty.Space,
        Content = Content.Select(b => b.CreateWidget()).ToArray()
      };
    }
    
    internal Block[] Content { get; set; }
    internal Empty _empty { get; set; }
  }
}