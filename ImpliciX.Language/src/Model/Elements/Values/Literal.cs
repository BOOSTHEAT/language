using System;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [Obsolete("Use Text10, Text50, Text200 instead")]
    [ValueObject]
    public readonly struct Literal: IPublicValue, ITextValue
    {
        [ModelFactoryMethod]
        public static Result<Literal> FromString(string value) => new Literal(value);

        public static Literal Create(string value) => new(value);

        private string Value { get; }

        private Literal(string value)
        {
            Value = value.Length > MaxLength ? value[..MaxLength] : value;
        }

        public override string ToString() => Value;

        public int MaxLength { get; } = 200;

        public object PublicValue() => Value;
    }
}