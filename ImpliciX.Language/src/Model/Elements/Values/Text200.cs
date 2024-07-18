using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model;

[ValueObject]
public readonly struct Text200: IPublicValue, ITextValue
{
    [ModelFactoryMethod]
    public static Result<Text200> FromString(string value) => new Text200(value);

    public static Text200 Create(string value) => new(value);

    private string Value { get; }

    private Text200(string value)
    {
        Value = value.Length > MaxLength ? value[..MaxLength] : value;
    }

    public override string ToString() => Value;

    public int MaxLength { get; } = 200;

    public object PublicValue() => Value;
}