using System;
using System.Linq;
using ImpliciX.Language.Driver;
namespace ImpliciX.Language.Modbus
{
    public class Command : IEquatable<Command>
    {
        public static Command Empty() => Create(0, new ushort[0]);
        public static Command Create(ushort startAddress, ushort[] data) =>
            new Command(startAddress, data);

        public ushort StartAddress { get; }
        public ushort[] DataToWrite { get; }

        public bool IsEmpty => DataToWrite.Length == 0;

        public Command WithState(IDriverState state)
        {
            _state = state;
            return this;
        }

        private IDriverState _state;

        public IDriverState State => _state;

        private Command(ushort startAddress, ushort[] data)
        {
            StartAddress = startAddress;
            DataToWrite = data;
        }

        public bool Equals(Command other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StartAddress == other.StartAddress
                   && DataToWrite.SequenceEqual(other.DataToWrite);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Command) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartAddress, DataToWrite);
        }

        public static bool operator ==(Command left, Command right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Command left, Command right)
        {
            return !Equals(left, right);
        }
    }
}