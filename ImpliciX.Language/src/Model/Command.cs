using System;

namespace ImpliciX.Language.Model
{
    [ModelObject]
    public class Command<T> : IModelCommand, IEquatable<Command<T>>
    {
        public Urn Urn { get; }
        public object Arg { get; }

        private Command(CommandUrn<T> urn, T arg)
        {
            Urn = urn;
            Arg = arg;
        }

        [ModelFactoryMethod]
        public static Command<T> Create(CommandUrn<T> urn, T arg)
        {
            return new Command<T>(urn, arg);
        }

        public bool Equals(Command<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Urn, other.Urn) && Equals(Arg, other.Arg);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Command<T>) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Urn, Arg);
        }
    }

    public interface IModelCommand
    {
         Urn Urn { get; }
         object Arg { get; }
    }
}