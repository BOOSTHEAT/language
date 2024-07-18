using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

internal sealed class Increment<T> : Block
{
  private readonly Block _visual;
  private readonly PropertyUrn<T> _inputUrn;
  private readonly double _stepValue;

  public Increment(Block visual, PropertyUrn<T> inputUrn, double incrementStepValue)
  {
    _visual = visual ?? throw new ArgumentNullException(nameof(visual));
    _inputUrn = inputUrn ?? throw new ArgumentNullException(nameof(inputUrn));
    _stepValue = incrementStepValue;
  }

  public override Widget CreateWidget()
  {
    return new IncrementWidget
    {
      Visual = _visual.CreateWidget(),
      InputUrn = PropertyFeed.Subscribe(_inputUrn),
      StepValue = _stepValue
    };
  }
}