using ImpliciX.Language.Control;

namespace ImpliciX.Language.Tests.Control.Examples.Functions
{
  public class Identity
  {
    public static FuncRef Func => new FuncRef(nameof(Identity), () => Runner, xUrns=>xUrns);
    public static FunctionRun Runner =>
      (functionDefinition, xs) => xs[0].value;
  }
}