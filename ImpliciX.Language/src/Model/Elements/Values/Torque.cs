using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
  [ValueObject]
  public readonly struct Torque : IFloat<Torque>, IPublicValue
  {
    public float Value { get; }

    [ModelFactoryMethod]
    public static Result<Torque> FromString(string value)
    {
      var isFloat = Single.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var f);
      if (!isFloat)
        return new InvalidValueError($"{value} is not valid for {nameof(Torque)}");
      return new Torque(f);
    }

    public static Result<Torque> FromFloat(float value) => new Torque(value);
    public static Torque Create(float value) => new(value);
    private Torque(float value) => Value = value;
    public override string ToString() => Value.ToString("F3", CultureInfo.InvariantCulture);
    public object PublicValue() => Value;
    public float ToFloat() => Value;
  }
}