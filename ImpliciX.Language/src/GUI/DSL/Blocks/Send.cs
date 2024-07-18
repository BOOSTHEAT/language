using System;
using ImpliciX.Language.Model;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.GUI;

internal sealed class Send : Block
{
  private readonly Block _visual;
  private readonly CommandUrn<NoArg> _commandUrn;

  public Send(Block visual, CommandUrn<NoArg> commandUrn)
  {
    _visual = visual ?? throw new ArgumentNullException(nameof(visual));
    _commandUrn = commandUrn ?? throw new ArgumentNullException(nameof(commandUrn));
  }

  public override Widget CreateWidget()
    => new SendWidget
    {
      Visual = _visual.CreateWidget(),
      CommandUrn = _commandUrn
    };
}