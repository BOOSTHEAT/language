using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct ExpansionValveAbsolutePosition: IPublicValue

    {
    private ushort Value { get; }

    private ExpansionValveAbsolutePosition(in ushort value)
    {
        Value = value;
    }

    [ModelFactoryMethod]
    public static Result<ExpansionValveAbsolutePosition> FromString(string value)
    {
        var isfloat = ushort.TryParse(value, out var mhs);
        if (!isfloat) return ValidationError(value);
        return FromUShort(mhs);
    }

    private static Result<ExpansionValveAbsolutePosition> FromUShort(ushort value)
    {
        if (value > 480) return ValidationError(value.ToString());
        return new ExpansionValveAbsolutePosition(value);
    }

    private static Result<ExpansionValveAbsolutePosition> ValidationError(string value) =>
        new InvalidValueError(
            $"Value: {value} is invalid. For type {nameof(ExpansionValveAbsolutePosition)} it should be an unsigned short between 0 and 480");

    public override string ToString() => $"{Value}";
    public object PublicValue()
    {
        return Value;
    }
    }
}