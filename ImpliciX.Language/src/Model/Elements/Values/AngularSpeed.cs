using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct AngularSpeed : IFloat<AngularSpeed>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<AngularSpeed> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(AngularSpeed)}");
            return new AngularSpeed(f);
        }

        private float Value { get; }

        public static Result<AngularSpeed> FromFloat(float speed)
        {
            return new AngularSpeed(speed);
        }

        private AngularSpeed(float speed)
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
            return obj is IEquatable<AngularSpeed> other && other.Equals(this);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(AngularSpeed left, AngularSpeed right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AngularSpeed left, AngularSpeed right)
        {
            return !left.Equals(right);
        }
    }
}