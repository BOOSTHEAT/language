namespace ImpliciX.Language.Model
{
    [UrnObject]
    public class CommandUrn<TArg> : Urn
    {
        protected CommandUrn(string value) : base(value)
        {
        }

        [ModelFactoryMethod]
        public static CommandUrn<TArg> Build(params string[] components)
        {
            return new CommandUrn<TArg>(Format(components));
        }

        public static implicit operator CommandUrn<TArg>(string value) => new CommandUrn<TArg>(value);

    }
}