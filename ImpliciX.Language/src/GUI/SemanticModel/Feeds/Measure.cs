using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI
{
  public class MeasureFeed : Node
  {
    protected MeasureFeed(Urn urn, Type type, UnitUsage unitUsage) : base(urn)
    {
      Type = type;
      ShowUnit = unitUsage;
    }
    public Type Type { get; }
    public UnitUsage ShowUnit { get; }

    public static MeasureFeed<T> Subscribe<T>(MeasureNode<T> node, UnitUsage unitUsage = UnitUsage.DoNotDisplayUnit) => new MeasureFeed<T>(node,unitUsage);

    public enum UnitUsage
    {
      DisplayUnit,
      DoNotDisplayUnit,
    }
  }
  
  public class MeasureFeed<T> : MeasureFeed
  {
    public MeasureFeed(MeasureNode<T> node, UnitUsage unitUsage)
    : base(node.Urn, typeof(T), unitUsage)
    {
    }
  }
}