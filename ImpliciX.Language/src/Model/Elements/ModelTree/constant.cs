namespace ImpliciX.Language.Model
{
    public class constant : RootModelNode
    {
        public constant() : base(nameof(constant))
        {
        }

        public static parameters parameters => new parameters(new constant());
    }
}
