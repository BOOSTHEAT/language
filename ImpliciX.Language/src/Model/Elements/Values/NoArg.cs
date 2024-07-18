using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public struct NoArg: IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<NoArg> FromString(string value) => default(NoArg);

        public override string ToString()
        {
            return default;
        }

        public object PublicValue()
        {
            return ToString();
        }
    }
}