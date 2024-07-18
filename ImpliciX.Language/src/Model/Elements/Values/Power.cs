using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Power : IFloat<Power>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<Power> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(Power)}");
            return new Power(f);
        }

        private float Value { get; }

        public static Result<Power> FromFloat(float power)
        {
            return new Power(power);
        }

        private Power(float power)
        {
            Value = power;
        }

        public override string ToString()
        {
            return Value.ToString("G", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return obj is IEquatable<Power> other && other.Equals(this);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Power left, Power right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Power left, Power right)
        {
            return !left.Equals(right);
        }
    }
}