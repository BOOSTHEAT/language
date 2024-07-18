using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

public sealed class Input : Block
{
    internal PropertyUrn<Literal> Urn { get; }

    public Input(PropertyUrn<Literal> urn)
    {
        Urn = urn ?? throw new ArgumentNullException(nameof(urn));
    }

    public override Widget CreateWidget() => new TextBox()
    {
        Value = PropertyFeed.Subscribe(Urn),
        Style = Font?.CreateStyle(),
        Width = WidthValue
    };
}