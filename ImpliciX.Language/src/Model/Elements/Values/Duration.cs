using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct Duration : IFloat<Duration>, IPublicValue
    {
        public static Result<Duration> FromFloat(float seconds)
        {
            return new Duration(seconds);
        }

        [ModelFactoryMethod]
        public static Result<Duration> FromString(string seconds)
        {
            return new Duration(float.Parse(seconds, CultureInfo.InvariantCulture));
        }

        private float Seconds { get; }
        public uint Milliseconds => (uint) (Seconds * 1000);

        private Duration(float seconds)
        {
            Seconds = seconds;
        }

        public override string ToString()
        {
            return $"{Seconds}";
        }

        public object PublicValue()
        {
            return Seconds;
        }

        public float ToFloat()
        {
            return Seconds;
        }
    }
}