namespace ImpliciX.Language.Model
{
  public class temperature : ModelNode
  {
    public temperature(ModelNode parent) : base(nameof(temperature), parent)
    {
    }
    public PropertyUrn<Temperature> zero => PropertyUrn<Temperature>.Build(Urn, nameof(zero));
  }
}