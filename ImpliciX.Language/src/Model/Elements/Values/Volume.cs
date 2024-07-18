using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Volume : IFloat<Volume>, IPublicValue
    {
        public static Result<Volume> FromFloat(float value) => new Volume(value);

        private static Result<Volume> ValidationError(string value)
        {
            return new InvalidValueError(
                $"Value: {value} is invalid. For type {nameof(Volume)} it should be a positive float");
        }

        [ModelFactoryMethod]
        public static Result<Volume> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var volume);
            if (!isfloat || volume < 0) return ValidationError(value);
            return new Volume(volume);
        }

        private Volume(float value)
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