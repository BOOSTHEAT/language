using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Counter : IFloat, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<Counter> FromString(string value) =>
            ulong.TryParse(value, out var integerValue)
                ? Result<Counter>.Create(FromInteger(integerValue))
                : new InvalidValueError(
                    $"Value: {value} is invalid. For type {nameof(Counter)} it should be a positive integer");
        public static Counter FromInteger(ulong value) => new Counter(value);
        public override string ToString() => Value.ToString();
        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat() => Value;
        
        private Counter(ulong value)
        {
            Value = value;
        }
        private ulong Value { get; }
    }
}