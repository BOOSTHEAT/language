using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
  [ValueObject]
  public readonly struct Length : IFloat<Length>, IPublicValue
  {
    public float Value { get; }

    [ModelFactoryMethod]
    public static Result<Length> FromString(string value)
    {
      var isFloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
      if (!isFloat)
        return new InvalidValueError($"{value} is not valid for {nameof(Length)}");
      return new Length(f);
    }

    public static Result<Length> FromFloat(float value) => new Length(value);
    public static Length Create(float value) => new(value);
    private Length(float value) => Value = value;
    public override string ToString() => Value.ToString("F3", CultureInfo.InvariantCulture);
    public object PublicValue() => Value;
    public float ToFloat() => Value;
  }
}