namespace ImpliciX.Language.Model
{
    public class parameters : ModelNode
    {
        public parameters(ModelNode parent) : base(nameof(parameters), parent)
        {
        }

        public PropertyUrn<FunctionDefinition> none => PropertyUrn<FunctionDefinition>.Build(Urn,nameof(none));
        public percentage percentage => new percentage(this);
        public displacement_queue displacement_queue => new displacement_queue(this);
        public power power => new power(this);
        public temperature temperature => new temperature(this);
    }
}