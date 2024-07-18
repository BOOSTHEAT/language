using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
  public class WatchdogModuleDefinition
  {
    public IDictionary<Urn,string> InputOutputPanic { get; set; }
    public CommandUrn<NoArg> Restart { get; set; }
  }
}