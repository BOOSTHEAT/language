namespace ImpliciX.Language.Model
{
  public class power : ModelNode
  {
    public power(ModelNode parent) : base(nameof(power), parent)
    {
    }

    public PropertyUrn<Power> zero => PropertyUrn<Power>.Build(Urn, nameof(zero));
  }
}