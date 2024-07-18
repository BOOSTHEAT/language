using System;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct PackageLocation: IPublicValue
    {
        public Uri Value { get; }

        [ModelFactoryMethod]
        public static Result<PackageLocation> FromString(string value) =>
            Result<PackageLocation>.Create(new PackageLocation(new Uri(value)));

        public PackageLocation(Uri value)
        {
            Value = value;
        }

        public override string ToString() => $"{Value}";
        public object PublicValue()
        {
            return ToString();
        }

        private bool Equals(PackageLocation other)
        {
            return Equals(Value.ToString(), other.Value.ToString());
        }

        public override bool Equals(object obj)
        {
            return obj is PackageLocation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}