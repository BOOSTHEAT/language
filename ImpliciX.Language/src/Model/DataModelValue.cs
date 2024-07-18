using System;
using System.Collections.Generic;

namespace ImpliciX.Language.Model
{
    public class DataModelValue<T> : IIMutableDataModelValue
    {
        public DataModelValue(Urn urn, T value, TimeSpan at)
        {
            At = at;
            Urn = urn;
            Value = value;
        }

        public TimeSpan At { get; protected set; }
        public Urn Urn { get; protected set; }
        public T Value { get; protected set; }
        
        public IIMutableDataModelValue WithUrn(Urn urn)
        {
            return new DataModelValue<T>(urn, Value, At);
        }

        public IIMutableDataModelValue WithAt(TimeSpan at)
        {
            return new DataModelValue<T>(Urn, Value, at);
        }
        
        public IIMutableDataModelValue With(Urn urn, TimeSpan at)
        {
            return new DataModelValue<T>(urn, Value, at);
        }

        public object ModelValue()
        {
            return Value;
        }

        protected bool Equals(DataModelValue<T> other)
        {
            return Equals(Urn, other.Urn) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataModelValue<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (1 * 397) ^ (Urn != null ? Urn.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{new DateTime(At.Ticks):G} -> {Urn} = {Value}";
        }
    }
}