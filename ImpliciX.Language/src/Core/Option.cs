using System;
using System.Collections.Generic;

namespace ImpliciX.Language.Core
{
    public class Option<TValue>
    {
        private readonly TValue _some;
        private readonly bool _isSome;
        private Option() { }

        private Option(TValue some)
        {
            _some = some;
            _isSome = true;
        }

        public static implicit operator Option<TValue>(TValue value) => value == null
            ? None()
            : Some(value);

        public static Option<TValue> None() => new Option<TValue>();

        public static Option<TValue> Some(TValue value)
        {
            LContract.PreCondition(() => value != null, () => "value can't be null");
            return new Option<TValue>(value);
        }

        public bool IsSome => _isSome;
        public bool IsNone => !_isSome;

        public TValue GetValueOrDefault(TValue defaultValue) => _isSome
            ? _some
            : defaultValue;

        public TResult Match<TResult>(Func<TResult> whenNone, Func<TValue, TResult> whenSome)
        {
            return _isSome
                ? whenSome(_some)
                : whenNone();
        }

        public TValue GetValue()
        {
            return GetValueOrDefault(default(TValue));
        }

        protected bool Equals(Option<TValue> other)
        {
            if (other.IsNone ^ this.IsNone) return false;
            if (other.IsNone && this.IsNone) return true;
            return EqualityComparer<TValue>.Default.Equals(_some, other._some);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Option<TValue>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(_some);
        }
    }
}