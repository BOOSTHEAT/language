using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
  [ValueObject]
  public readonly struct Mass : IFloat<Mass>, IPublicValue
  {
    public static Result<Mass> FromFloat(float value) => new Mass(value);

    private static Result<Mass> ValidationError(string value)
    {
      return new InvalidValueError(
        $"Value: {value} is invalid. For type {nameof(Mass)} it should be a positive float");
    }

    [ModelFactoryMethod]
    public static Result<Mass> FromString(string value)
    {
      var isfloat = float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var volume);
      if (!isfloat || volume < 0) return ValidationError(value);
      return new Mass(volume);
    }

    private Mass(float value)
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