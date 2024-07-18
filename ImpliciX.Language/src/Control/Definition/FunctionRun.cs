using System;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
  public delegate float FunctionRun(FunctionDefinition definition, (float value, TimeSpan at)[] xs);
}