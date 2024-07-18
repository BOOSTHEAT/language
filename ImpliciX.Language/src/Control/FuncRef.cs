using System;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
  public class FuncRef
  {
    public Func<FunctionRun> Runner { get; }
    public Func<Urn[], Urn[]> TriggerSelector { get; }
    public string Name { get; }

    public FuncRef(string name, Func<FunctionRun> runner, Func<Urn[], Urn[]> triggerSelector)
    {
      Name = name;
      Runner = runner;
      TriggerSelector = triggerSelector;
    }
  }
}