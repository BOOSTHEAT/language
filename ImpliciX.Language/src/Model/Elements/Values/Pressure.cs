using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Pressure : IFloat<Pressure>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<Pressure> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var pascals);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(Pressure)}");
            return FromFloat(pascals);
        }

        public static Result<Pressure> FromFloat(float pascals)
        {
            if (pascals < 0)
                 new InvalidValueError($"Pressure value should be a positive value. Current input: {pascals}");
            return new Pressure(pascals);
        }

        private Pressure(float pascals)
        {
            Pascals = pascals;
        }

        private float Pascals { get; }

        public float ToFloat() => Pascals;

        public override string ToString()
        {
            return Pascals.ToString("F2", CultureInfo.InvariantCulture);
        }

        public object PublicValue()
        {
            return Pascals;
        }
    }
}