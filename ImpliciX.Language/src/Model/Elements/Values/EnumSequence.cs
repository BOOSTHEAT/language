using System.Linq;
namespace ImpliciX.Language.Model
{
    public struct EnumSequence
    {
        public System.Enum[] Items;

        public static EnumSequence Create(System.Enum[] items) => new EnumSequence {Items = items};

        private bool Equals(EnumSequence other)
        {
            return Items.SequenceEqual(other.Items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((EnumSequence) obj);
        }

        public override int GetHashCode()
        {
            return (Items != null ? Items.GetHashCode() : 0);
        }

        public bool Contains<TEnum>(TEnum item) where TEnum : System.Enum
        {
            return Items.Any(s => s.Equals(item));
        } 
    }
}