using System;
namespace ImpliciX.Language.Core
{
    public class Result2<TValue,TBoth>
    {
        private readonly Error _error;
        private readonly TValue _value;
        private readonly TBoth _both;

        private Result2(Error error, TBoth both)
        {
            _error = error;
            _value = default(TValue);
            _both = both;

        }

        private Result2(TValue value, TBoth both)
        {
            _error = null;
            _value = value;
            _both = both;
        }

        public bool IsSuccess => _error == null;
        public bool IsError => _error != null;

        public TValue Value => _value;
        
        public Error Error => _error;

        public TBoth Both => _both;

        public static Result2<TValue,TBoth> Create(Error error, TBoth both)
        {
            return new Result2<TValue,TBoth>(error, both);
        }

        public static Result2<TValue,TBoth> Create(TValue value, TBoth both)
        {
            return new Result2<TValue,TBoth>(value, both);
        }

        public TValue GetValueOrDefault(TValue defaultValue) => IsSuccess ? _value : defaultValue;
        public TValue GetValueOrDefault() => IsSuccess ? _value : default(TValue);

        public object Extract() => IsSuccess ? (object) _value : _error;
        
        public static implicit operator Result2<TValue, TBoth>((Error error,TBoth both) p) => new Result2<TValue,TBoth>(p.error,p.both);
        public static implicit operator Result2<TValue, TBoth>((TValue value, TBoth both) p) => new Result2<TValue,TBoth>(p.value, p.both);

        public TResult Match<TResult>(Func<Error, TBoth, TResult> whenError, Func<TValue, TBoth, TResult> whenSuccess)
            => this.IsError ? whenError(_error,_both) : whenSuccess(_value,_both);

 
    }

    public static class Result2Extensions
    {
        public static Result2<U,B> ToResult2<U,B>(this U @this, B b)
        {
            if(@this is Error error) return Result2<U,B>.Create(error,b);
            return Result2<U,B>.Create(@this,b);
        }
            
        public static Result2<TResult, TBoth> Map<TValue, TBoth, TResult>(this Result2<TValue,TBoth> @this,
            Func<(TValue, TBoth), (TResult,TBoth)> fnTransform)
        {
            return @this.Match(
                (err,both) => { return Result2<TResult,TBoth>.Create(err,both); },
                (value,both) => fnTransform((value,both)));
        }

        
        public static Result2<TResult,TBoth> Bind<TValue, TBoth, TResult>(this Result2<TValue,TBoth> @this,
            Func<(TValue, TBoth), Result2<TResult,TBoth>> fn)
        {
            return @this.Match(
                (err, both) => { return Result2<TResult,TBoth>.Create(err,both); },
                (value,both) => fn((value,both)));
        }

        public static Result2<TSource,TBoth> Tap<TSource,TBoth>(this Result2<TSource,TBoth> @this, Action<TSource> whenSuccess)
            => @this.Tap(_ => { }, whenSuccess);
        public static Result2<TSource,TBoth> Tap<TSource,TBoth>(this Result2<TSource,TBoth> @this, Action<Error> whenError,
            Action<TSource> whenSuccess)
        {
            if (@this.IsError)
            {
                whenError(@this.Error);
            }

            if (@this.IsSuccess)
            {
                whenSuccess(@this.Value);
            }

            return @this;
        }

        public static Result2<TResult,TBoth> Select<TValue, TBoth, TResult>(this Result2<TValue,TBoth> @this,
            Func<(TValue value, TBoth both), (TResult,TBoth)> fnTransform) => @this.Map(fnTransform);

         public static Result2<TResult,TBoth> SelectMany<TValue, TBoth, TResult>(this Result2<TValue,TBoth> @this,
             Func<(TValue value,TBoth both), Result2<TResult,TBoth>> fn) => @this.Bind(fn);
        
          public static Result2<TResult2,TBoth> SelectMany<TValue, TBoth, TResult, TResult2>(this Result2<TValue,TBoth> @this,
              Func<(TValue ,TBoth), Result2<TResult,TBoth>> fn, Func<(TValue ,TBoth), (TResult ,TBoth), (TResult2 ,TBoth)> comp)
              => @this.Bind(t => fn(t).Bind<TResult, TBoth, TResult2>(u => comp(t, u)));
        
          public static Result2<TValue,TBoth> ThrowOnError<TValue,TBoth>(this Result2<TValue,TBoth> @this)
          {
              if (@this.IsError) throw new Exception(@this.Error.Message);
              return @this;
          }

    }
}