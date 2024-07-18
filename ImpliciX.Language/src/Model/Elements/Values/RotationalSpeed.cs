using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct RotationalSpeed : IFloat<RotationalSpeed>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<RotationalSpeed> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(RotationalSpeed)}");
            return new RotationalSpeed(f);
        }

        public float Value { get; }

        public static Result<RotationalSpeed> FromFloat(float speed)
        {
            return new RotationalSpeed(speed);
        }

        private RotationalSpeed(float speed)
        {
            Value = speed;
        }

        public override string ToString()
        {
            return Value.ToString("F3", CultureInfo.InvariantCulture);
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
            return obj is IEquatable<RotationalSpeed> other && other.Equals(this);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(RotationalSpeed left, RotationalSpeed right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RotationalSpeed left, RotationalSpeed right)
        {
            return !left.Equals(right);
        }
    }
}