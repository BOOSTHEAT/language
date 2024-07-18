using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct DifferentialTemperature : IFloat<DifferentialTemperature>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<DifferentialTemperature> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var kelvin);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(DifferentialTemperature)}");
            return FromFloat(kelvin);
        }

        public static Result<DifferentialTemperature> FromFloat(float kelvin)
        {
            return new DifferentialTemperature(kelvin);
        }

        private DifferentialTemperature(float kelvin)
        {
            Kelvins = kelvin;
        }

        private float Kelvins { get; }

        public float ToFloat() => Kelvins;

        public override string ToString()
        {
            return Kelvins.ToString("F2", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Kelvins;
        }
    }
}