using System;
using System.Collections.Generic;
namespace ImpliciX.Language.Core
{
  public class Result<TValue> : IEquatable<Result<TValue>>
  {
    private readonly Error _error;
    private readonly TValue _value;

    private Result(Error exception)
    {
      _error = exception;
      _value = default(TValue);
    }

    private Result(TValue value)
    {
      _error = null;
      _value = value;
    }

    public bool IsSuccess => _error == null;
    public bool IsError => _error != null;

    public TValue Value => _value;
    public Error Error => _error;

    public static Result<TValue> Create(Error error)
    {
      return new Result<TValue>(error);
    }

    public static Result<TValue> Create(TValue value)
    {
      return new Result<TValue>(value);
    }

    public TValue GetValueOrDefault(TValue defaultValue) => IsSuccess ? _value : defaultValue;
    public TValue GetValueOrDefault() => IsSuccess ? _value : default(TValue);


    // ReSharper disable once UnusedMember.Global
    // used by reflection by model factory 
    public object Extract() => IsSuccess ? (object)_value : _error;
    public static implicit operator Result<TValue>(Error error) => new Result<TValue>(error);
    public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value);

    public TResult Match<TResult>(Func<Error, TResult> whenError, Func<TValue, TResult> whenSuccess)
      => this.IsError ? whenError(_error) : whenSuccess(_value);

    public bool Equals(Result<TValue> other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(_error, other._error) &&
             Equals(_value, other._value);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Result<TValue>)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((_error != null ? _error.GetHashCode() : 0) * 397) ^
               EqualityComparer<TValue>.Default.GetHashCode(_value);
      }
    }

    public static bool operator ==(Result<TValue> left, Result<TValue> right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Result<TValue> left, Result<TValue> right)
    {
      return !Equals(left, right);
    }

    public override string ToString() => IsSuccess ? $"Success[{Value}]" : $"Error[{Error.Message}]";
  }
}