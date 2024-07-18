#nullable enable
using System;
using System.Drawing;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.GUI;

public class DecoratedUrn
{
  public Urn Urn { get; }
  public Font? LabelFont { get; private set; }
  public Color? FillColor { get; private set; }

  public DecoratedUrn(Urn urn)
  {
    Urn = urn ?? throw new ArgumentNullException(nameof(urn));
  }

  public DecoratedUrn Fill(Color fillColor)
  {
    FillColor = fillColor;
    return this;
  }

  public DecoratedUrn With(Font labelFont)
  {
    LabelFont = labelFont ?? throw new ArgumentNullException(nameof(labelFont));
    return this;
  }

  internal FeedDecoration ToFeedDecorationAsTimeSeries() =>
    new(
      TimeSeriesFeed.Subscribe(Urn),
      this.LabelFont?.CreateStyle(),
      this.FillColor);

  internal virtual FeedDecoration ToFeedDecoration() =>
    throw new ApplicationException("Cannot subscribe to property with unknown type");
}

internal class DecoratedPropertyUrn<T> : DecoratedUrn
{
  private PropertyUrn<T> UrnT { get; }

  internal DecoratedPropertyUrn(PropertyUrn<T> urn) : base(urn) => UrnT = urn;

  internal override FeedDecoration ToFeedDecoration() =>
    new(
      PropertyFeed.Subscribe(UrnT),
      this.LabelFont?.CreateStyle(),
      this.FillColor);
}

internal class DecoratedMeasureUrn<T> : DecoratedUrn
{
  private MeasureNode<T> Node { get; }

  internal DecoratedMeasureUrn(MeasureNode<T> node) : base(node.Urn) => Node = node;

  internal override FeedDecoration ToFeedDecoration() =>
    new(
      MeasureFeed.Subscribe(Node),
      this.LabelFont?.CreateStyle(),
      this.FillColor);
}
