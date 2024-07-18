namespace ImpliciX.Language.Model
{
    public class SubSystemApiNode : SubSystemNode
    {
        public SubSystemApiNode(string urnToken, ModelNode parent) : base(urnToken, parent)
        {
        }

        public CommandUrn<SubsystemState> _jump =>  CommandUrn<SubsystemState>.Build(Urn, "JUMP");
    }
}