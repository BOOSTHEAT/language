using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed class DropDownList<T> : Block
{
  internal PropertyUrn<T> Urn { get; }

  public DropDownList(PropertyUrn<T> urn)
  {
    Urn = urn ?? throw new ArgumentNullException(nameof(urn));
  }

  public override Widget CreateWidget() => new DropDownListWidget
  {
    Width = WidthValue,
    Value = PropertyFeed.Subscribe(Urn)
  };
}