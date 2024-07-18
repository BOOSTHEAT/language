using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Temperature : IFloat<Temperature>, IPublicValue
    {
        public float Degrees { get; }

        [ModelFactoryMethod]
        public static Result<Temperature> FromString(string value)
        {
            var isFloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
            if (!isFloat) return new InvalidValueError($"{value} is not valid for {nameof(Temperature)}");
            return new Temperature(f);
        }

        public static Result<Temperature> FromFloat(float value)
        {
            return new Temperature(value);
        }

        public static Temperature Create(float degrees)
        {
            return new Temperature(degrees);
        }

        private Temperature(float degrees)
        {
            Degrees = degrees;
        }

        public override string ToString()
        {
            return Degrees.ToString("F1", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Degrees;
        }

        public float ToFloat()
        {
            return Degrees;
        }
    }
}