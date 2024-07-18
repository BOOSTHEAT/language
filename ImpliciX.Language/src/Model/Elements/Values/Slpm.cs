using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct StandardLiterPerMinute : IFloat<StandardLiterPerMinute>, IPublicValue
    {
        public static Result<StandardLiterPerMinute> FromFloat(float value) => new StandardLiterPerMinute(value);
        
        private static Result<StandardLiterPerMinute> ValidationError(string value)
        {
            return new InvalidValueError($"Value: {value} is invalid. For type {nameof(StandardLiterPerMinute)}");
        }


        [ModelFactoryMethod]
        public static Result<StandardLiterPerMinute> FromString(string value)
        {
            var isFloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var slpm);
            return !isFloat ? ValidationError(value) : FromFloat(slpm);
        }

        private StandardLiterPerMinute(float value)
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
    }
}