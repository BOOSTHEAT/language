using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Flow : IFloat<Flow>, IPublicValue
    {
        public static Result<Flow> FromFloat(float value) => new Flow(value);

        private static Result<Flow> ValidationError(string value)
        {
            return new InvalidValueError(
                $"Value: {value} is invalid. For type {nameof(Flow)} it should be a positive float");
        }

        [ModelFactoryMethod]
        public static Result<Flow> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var volume);
            if (!isfloat || volume < 0) return ValidationError(value);
            return FromFloat(volume);
        }

        private Flow(float value)
        {
            Value = value;
        }

        private float Value { get; }

        public override string ToString()
        {
            return Value.ToString("F9", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat() => Value;
    }
}