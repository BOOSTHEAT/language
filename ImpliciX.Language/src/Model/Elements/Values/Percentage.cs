using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Percentage : IFloat<Percentage>, IPublicValue
    {
        public static readonly Percentage ONE = new Percentage(1f); 
        public static Result<Percentage> FromFloat(float value) => new Percentage(value);

        private static Result<Percentage> ValidationError(string value)
        {
            return new InvalidValueError(
                $"Value: {value} is invalid. For type {nameof(Percentage)} it should be a float");
        }

        [ModelFactoryMethod]
        public static Result<Percentage> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var pct);
            if (!isfloat) return ValidationError(value);
            return FromFloat(pct);
        }

        private Percentage(float value)
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
        

        public override bool Equals(object obj)
        {
            return obj is IEquatable<Percentage> other && other.Equals(this);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}