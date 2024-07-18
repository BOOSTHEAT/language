namespace ImpliciX.Language.Model
{
    public class MeasureNode<T> : ModelNode
    {
        public MeasureNode(Urn urnToken, ModelNode parent) : base(urnToken, parent)
        {
            measure = PropertyUrn<T>.Build(Urn, nameof(measure));
            status = PropertyUrn<MeasureStatus>.Build(Urn, nameof(status));
        }

        public PropertyUrn<T> measure { get; }
        public PropertyUrn<MeasureStatus> status { get; }

    }
}