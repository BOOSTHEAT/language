using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

internal class DataDrivenImageMeasure<T> : Image
{
  private readonly MeasureNode<T> _measure;
  private readonly double _floor;
  private readonly double _step;

  public DataDrivenImageMeasure(MeasureNode<T> measure, double floor, double step)
  {
    _measure = measure;
    _floor = floor;
    _step = step;
  }
  
  public override Widget CreateWidget()
  {
    return new DataDrivenImageWidget
    {
      Path = Const.Is(Path),
      Value = MeasureFeed.Subscribe(_measure),
      Floor = Const.Is(_floor),
      Step = Const.Is(_step),
    };
  }
}

internal class DataDrivenImageProperty<T> : Image
{
  private readonly PropertyUrn<T> _property;
  private readonly double _floor;
  private readonly double _step;

  public DataDrivenImageProperty(PropertyUrn<T> property, double floor, double step)
  {
    _property = property;
    _floor = floor;
    _step = step;
  }
  
  public override Widget CreateWidget()
  {
    return new DataDrivenImageWidget
    {
      Path = Const.Is(Path),
      Value = PropertyFeed.Subscribe(_property),
      Floor = Const.Is(_floor),
      Step = Const.Is(_step),
    };
  }
}