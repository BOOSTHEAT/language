namespace ImpliciX.Language.Model
{
    public class SubSystemNode : ModelNode
    {
        public SubSystemNode(string urnToken, ModelNode parent) : base(urnToken, parent)
        {
        }
        public PropertyUrn<SubsystemState> state => PropertyUrn<SubsystemState>.Build(Urn, nameof(state));

        public CommandUrn<NoArg> _activate => CommandUrn<NoArg>.Build(Urn, "ACTIVATE");
        public Urn id => Urn;
    }
}