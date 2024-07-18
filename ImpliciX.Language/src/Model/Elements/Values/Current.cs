using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct  Current : IFloat<Current>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<Current> FromString(string value)
        {
            var isfloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var amperes);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(DifferentialPressure)}");
            return FromFloat(amperes);
        }

        public static Result<Current> FromFloat(float amperes)
        {
            return new Current(amperes);
        }

        private Current(float amperes)
        {
            Amperes = amperes;
        }

        public float Amperes { get; }

        public float ToFloat() => Amperes;

        public override string ToString()
        {
            return Amperes.ToString("F2", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Amperes;
        }
    }
}