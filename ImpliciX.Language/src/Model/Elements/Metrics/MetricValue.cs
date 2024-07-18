using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct MetricValue : IFloat, IPublicValue
    {
        public readonly float Value;

        public TimeSpan SamplingStartDate { get; }
        public TimeSpan SamplingEndDate { get; }

        public MetricValue(float value, TimeSpan samplingStartDate, TimeSpan samplingEndDate)
        {
            Value = value;
            SamplingStartDate = samplingStartDate;
            SamplingEndDate = samplingEndDate;
        }
        
        [ModelFactoryMethod]
        public static Result<MetricValue> FromString(string value)
        {
            var isFloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
            if (!isFloat) return new InvalidValueError($"{value} is not valid for {nameof(MetricValue)}");
            return new MetricValue(f, TimeSpan.Zero, TimeSpan.Zero);
        }

        public override string ToString() => $"{Value}";
        public object PublicValue()
        {
            return Value;
        }

        public float ToFloat() => Value;

        private bool Equals(MetricValue other)
        {
            return Value.Equals(other.Value) && SamplingStartDate.Equals(other.SamplingStartDate) && SamplingEndDate.Equals(other.SamplingEndDate);
        }

        public override bool Equals(object obj)
        {
            return obj is MetricValue other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, SamplingStartDate, SamplingEndDate);
        }
    }
}