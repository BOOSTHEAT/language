#nullable enable
using System;
using System.Globalization;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
  public interface IFloat
  {
    float ToFloat();
  }

  public interface IFloat<T> : IEquatable<T>, IFloat
  {
    // TODO: uncomment when csproj supports static abstract in interfaces
    // static abstract Result<T> FromString(string value);
    // static abstract Result<T> FromFloat(float value);

    bool IEquatable<T>.Equals(T? other) =>
      other is IFloat<T> eqt
      && (ToFloat().Equals(eqt.ToFloat()) || Math.Abs(ToFloat() - eqt.ToFloat()) < 0.001f);
  }
}