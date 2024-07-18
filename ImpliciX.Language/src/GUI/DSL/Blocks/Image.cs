using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI
{
  public class Image : Block
  {
    internal string Path { get; set; }
    
    public Block DataDriven<T>(MeasureNode<T> measure, double floor, double step)
    {
      return new DataDrivenImageMeasure<T>(measure,floor,step) { Path = Path };
    }
    
    public Block DataDriven<T>(PropertyUrn<T> property, double floor, double step)
    {
      return new DataDrivenImageProperty<T>(property,floor,step) { Path = Path };
    }
    
    public override Widget CreateWidget()
    {
      return new ImageWidget
      {
        Path = Const.Is(Path)
      };
    }
  }
}