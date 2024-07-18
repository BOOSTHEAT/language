namespace ImpliciX.Language.Model
{
    public class AnalyticsCommunicationCountersNode : ModelNode
    {
        public AnalyticsCommunicationCountersNode(string name, ModelNode parent) : base(name, parent)
        {
            request_count = PropertyUrn<Counter>.Build(Urn, nameof(request_count));
            failed_request_count = PropertyUrn<Counter>.Build(Urn, nameof(failed_request_count));
            fatal_request_count = PropertyUrn<Counter>.Build(Urn, nameof(fatal_request_count));
        }
        
        public PropertyUrn<Counter> request_count { get; }
        public PropertyUrn<Counter> failed_request_count { get; }
        public PropertyUrn<Counter> fatal_request_count { get; }
    }
}