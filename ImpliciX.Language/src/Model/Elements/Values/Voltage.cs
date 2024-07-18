using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct  Voltage : IFloat<Voltage>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<Voltage> FromString(string value)
        {
            var isfloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var volts);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(DifferentialPressure)}");
            return FromFloat(volts);
        }

        public static Result<Voltage> FromFloat(float volts)
        {
            return new Voltage(volts);
        }

        private Voltage(float volts)
        {
            Volts = volts;
        }

        public float Volts { get; }

        public float ToFloat() => Volts;

        public override string ToString()
        {
            return Volts.ToString("F2", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Volts;
        }
    }
}