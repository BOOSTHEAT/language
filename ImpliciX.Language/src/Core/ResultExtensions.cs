using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpliciX.Language.Core;

public static class ResultExtensions
{
    public static Result<U> ToResult<U>(this U @this)
    {
        if (@this is Error error) return Result<U>.Create(error);
        return Result<U>.Create(@this);
    }

    public static Result2<U, B> ToResult2<U, B>(this Result<U> @this, B b)
    {
        return @this.Match(error => Result2<U, B>.Create(error, b), value => Result2<U, B>.Create(value, b));
    }

    public static Option<T> ValueToOption<T>(this Result<T> result)
        => result.IsSuccess
            ? result.Value
            : Option<T>.None();

    public static Result<TResult> Map<TValue, TResult>(this Result<TValue> @this, Func<TValue, TResult> fnTransform)
    {
        return @this.Match(Result<TResult>.Create, value => fnTransform(value));
    }

    public static Result<TResult> Bind<TValue, TResult>(this Result<TValue> @this, Func<TValue, Result<TResult>> fn)
    {
        return @this.Match(Result<TResult>.Create, fn);
    }


    public static Result<TSource> UnWrap<TSource>(this Result<Result<TSource>> @this)
    {
        return @this.Match(
            Result<TSource>.Create,
            res => res.Match(Result<TSource>.Create, Result<TSource>.Create)
        );
    }

    public static Result<TSource> Tap<TSource>(this Result<TSource> @this, Action<TSource> whenSuccess)
    {
        return @this.Tap(_ => { }, whenSuccess);
    }

    public static Result<TSource> Tap<TSource>(this Result<TSource> @this, Action<Error> whenError,
        Action<TSource> whenSuccess)
    {
        if (@this.IsError) whenError(@this.Error);

        if (@this.IsSuccess) whenSuccess(@this.Value);

        return @this;
    }

    public static Result<TResult> Select<TValue, TResult>(this Result<TValue> @this, Func<TValue, TResult> fnTransform)
    {
        return @this.Map(fnTransform);
    }

    public static Result<TResult> SelectMany<TValue, TResult>(this Result<TValue> @this, Func<TValue, Result<TResult>> fn)
    {
        return @this.Bind(fn);
    }

    public static Result<TResult2> SelectMany<TValue, TResult, TResult2>(this Result<TValue> @this,
        Func<TValue, Result<TResult>> fn, Func<TValue, TResult, TResult2> comp)
    {
        return @this.Bind(t => fn(t).Bind<TResult, TResult2>(u => comp(t, u)));
    }

    public static IEnumerable<T> Values<T>(this IEnumerable<Result<T>> @this)
    {
        return @this.Where(r => r.IsSuccess).Select(r => r.Value);
    }

    public static IEnumerable<Error> Errors<T>(this IEnumerable<Result<T>> @this)
    {
        return @this.Where(r => r.IsError).Select(r => r.Error);
    }

    public static Result<IEnumerable<T>> Traverse<T>(this IEnumerable<Result<T>> @this)
    {
        var items = @this.ToArray();
        var errors = items.Errors().ToList();
        return errors.Any()
            ? errors.Aggregate((e1, e2) => e1.Merge(e2))
            : Result<IEnumerable<T>>.Create(items.Values().ToList());
    }

    public static (IEnumerable<Error> errors, IEnumerable<T> outcome) SeparateResults<T>(this IEnumerable<Result<T>> results)
    {
        var errors = new List<Error>();
        var outcome = new List<T>();
        foreach (var result in results)
        {
            if (result.IsError)
            {
                errors.Add(result.Error);
            }
            else
            {
                outcome.Add(result.GetValueOrDefault());
            }
        }

        return (errors, outcome);
    }

    public static Result<T[]> Traverse<T>(this Result<T>[] @this)
    {
        var items = @this.ToArray();
        var errors = items.Errors().ToList();
        return errors.Any()
            ? errors.Aggregate((e1, e2) => e1.Merge(e2))
            : Result<T[]>.Create(items.Values().ToArray());
    }

    public static Result<T> RCast<T>(this object source)
    {
        if (source is T sourceAsT)
            return Result<T>.Create(sourceAsT);

        return Result<T>.Create(new Error("Invalid cast",
            $"{source.GetType().Name} cannot be cast into {typeof(T).Name}"));
    }

    public static Result<T> RInvoke<T>(this Func<T> f, Func<Exception, Error> errorFunc)
    {
        try
        {
            return f();
        }
        catch (Exception e)
        {
            return errorFunc(e);
        }
    }

    public static Result<T> ThrowOnError<T>(this Result<T> @this)
    {
        if (@this.IsError) throw new Exception(@this.Error.Message);
        return @this;
    }
}