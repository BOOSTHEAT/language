#nullable enable
using System;

namespace ImpliciX.Language.Core;

public static class OptionExtensions
{
    public static Option<TSource> ToOption<TSource>(this TSource @this)
    {
        return @this == null
            ? Option<TSource>.None()
            : Option<TSource>.Some(@this);
    }

    public static Option<TResult> Map<TSource, TResult>(this Option<TSource> @this, Func<TSource, TResult> fn)
    {
        return @this.Match(
            Option<TResult>.None,
            (t) => Option<TResult>.Some(fn(t)));
    }

    public static Option<TSource> Tap<TSource>(this Option<TSource> @this, Action<TSource> whenSome)
        => @this.Tap(() => { }, whenSome);

    public static Option<TSource> Tap<TSource>(this Option<TSource> @this, Action whenNone,
        Action<TSource> whenSome)
    {
        if (@this.IsNone)
        {
            whenNone();
        }

        if (@this.IsSome)
        {
            whenSome(@this.GetValue());
        }

        return @this;
    }

    public static Option<TResult> Bind<TSource, TResult>(this Option<TSource> @this, Func<TSource, Option<TResult>> fn)
    {
        return @this.Match(
            Option<TResult>.None,
            fn);
    }

    public static Result<TSource> ToResult<TSource>(this Option<TSource> @this, string? errorMessage = null)
    {
        return @this.Match(() => Result<TSource>.Create(new Error("NullOutcome", errorMessage ?? "The outcome is null")), (value) => value);
    }

    public static Option<TResult> Select<TValue, TResult>(this Option<TValue> @this,
        Func<TValue, TResult> fnTransform) => @this.Map(fnTransform);

    public static Option<TResult> SelectMany<TValue, TResult>(this Option<TValue> @this,
        Func<TValue, Option<TResult>> fn) => @this.Bind(fn);

    public static Option<TResult2> SelectMany<TValue, TResult, TResult2>(this Option<TValue> @this,
        Func<TValue, Option<TResult>> fn, Func<TValue, TResult, TResult2> comp)
        => @this.Bind(t => fn(t).Bind<TResult, TResult2>(u => comp(t, u)));
}