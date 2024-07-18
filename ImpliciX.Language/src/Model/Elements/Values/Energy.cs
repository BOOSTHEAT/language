using System.Globalization;
using System.Runtime.Serialization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Energy : IFloat<Energy>, IPublicValue
    {
        private static Result<Energy> ValidationError(string value)
        {
            return new InvalidValueError(
                $"Value: {value} is invalid. For type {nameof(Energy)} it should be a positive float");
        }

        [ModelFactoryMethod]
        public static Result<Energy> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var energy);
            if (!isfloat || energy < 0) return ValidationError(value);
            return new Energy(energy);
        }

        public static Result<Energy> FromFloat(float value)=> new Energy(value);

        private Energy(float value)
        {
            Value = value;
        }

        private float Value { get; }

        public override string ToString()
        {
            return Value.ToString("F3", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat() => Value;
    }
}