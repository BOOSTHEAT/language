using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model;

[ValueObject]
public readonly struct Text10: IPublicValue, ITextValue
{
    [ModelFactoryMethod]
    public static Result<Text10> FromString(string value) => new Text10(value);

    public static Text10 Create(string value) => new(value);

    private string Value { get; }

    private Text10(string value)
    {
        Value = value.Length > MaxLength ? value[..MaxLength] : value;
    }

    public override string ToString() => Value;

    public int MaxLength { get; } = 10;

    public object PublicValue() => Value;
}