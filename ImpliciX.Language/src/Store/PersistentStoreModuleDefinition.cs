using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Store
{
  public class PersistentStoreModuleDefinition 
  {
    public IDictionary<Urn, (string Name, string Value)[]> DefaultUserSettings { get; set; }
    public IDictionary<Urn, (string Name, string Value)[]> DefaultVersionSettings { get; set; }
    public CommandUrn<Literal> StartFirewall { get; set; }
    public CommandUrn<NoArg> StopFirewall { get; set; }
    
    public CommandNode<NoArg> CleanVersionSettings { get; set; }
    public IDictionary<string, IEnumerable<FirewallRule>> FirewallRules { get; set; }
  }
}