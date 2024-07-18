using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed class OnOffButton<T> : Block
{
  internal PropertyUrn<T> Urn { get; }

  public OnOffButton(PropertyUrn<T> urn)
  {
    Urn = urn ?? throw new ArgumentNullException(nameof(urn));
  }

  public override Widget CreateWidget() => new OnOffButtonWidget
  {
    Value = PropertyFeed.Subscribe(Urn)
  };
}