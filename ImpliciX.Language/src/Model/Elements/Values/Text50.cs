using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model;

[ValueObject]
public readonly struct Text50: IPublicValue, ITextValue
{
    [ModelFactoryMethod]
    public static Result<Text50> FromString(string value) => new Text50(value);
    public static Text50 Create(string value) => new(value);
    private string Value { get; }

    private Text50(string value)
    {
        Value = value.Length > MaxLength ? value[..MaxLength] : value;
    }
    public override string ToString() => Value;
    public int MaxLength { get; } = 50;
    public object PublicValue() => Value;
}