namespace ImpliciX.Language.Model
{
    public class percentage : ModelNode
    {
        public percentage(ModelNode parent) : base(nameof(percentage), parent)
        {
        }

        public PropertyUrn<Percentage> zero => PropertyUrn<Percentage>.Build(Urn, nameof(zero));
        public PropertyUrn<Percentage> one => PropertyUrn<Percentage>.Build(Urn, nameof(one));
        public PropertyUrn<Percentage> hundred => PropertyUrn<Percentage>.Build(Urn, nameof(hundred));
    }
}