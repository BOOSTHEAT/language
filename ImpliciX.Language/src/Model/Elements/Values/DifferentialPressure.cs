using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct DifferentialPressure : IFloat<DifferentialPressure>, IPublicValue
    {
        [ModelFactoryMethod]
        public static Result<DifferentialPressure> FromString(string value)
        {
            var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var pascals);
            if (!isfloat) return new InvalidValueError($"{value} is not valid for {nameof(DifferentialPressure)}");
            return FromFloat(pascals);
        }

        public static Result<DifferentialPressure> FromFloat(float pascals)
        {
            return new DifferentialPressure(pascals);
        }

        private DifferentialPressure(float pascals)
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