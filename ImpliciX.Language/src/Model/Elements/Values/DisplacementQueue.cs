using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct DisplacementQueue : IFloat<DisplacementQueue>, IPublicValue
    {
        private short Value { get; }

        private DisplacementQueue(in short value)
        {
            Value = value;
        }

        [ModelFactoryMethod]
        public static Result<DisplacementQueue> FromString(string value)
        {
            var success = short.TryParse(value, out var mhs);
            if (!success) return ValidationError(value);
            return FromShort(mhs);
        }

        public static Result<DisplacementQueue> FromShort(short value)
        {
            if (value < -480 || value > 480) return ValidationError(value.ToString());
            return new DisplacementQueue(value);
        }

        private static Result<DisplacementQueue> ValidationError(string value) =>
            new InvalidValueError(
                $"Value: {value} is invalid. For type {nameof(DisplacementQueue)} it should be an short between -480 and 480");

        public override string ToString() => $"{Value}";
        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat()
        {
            return Value;
        }
    }
}