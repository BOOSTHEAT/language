namespace ImpliciX.Language.Model
{
  public class displacement_queue : ModelNode
  {
    public displacement_queue(ModelNode parent) : base(nameof(displacement_queue), parent)
    {
    }

    public PropertyUrn<DisplacementQueue> zero => PropertyUrn<DisplacementQueue>.Build(Urn, nameof(zero));
    public PropertyUrn<DisplacementQueue> one => PropertyUrn<DisplacementQueue>.Build(Urn, nameof(one));
  }
}