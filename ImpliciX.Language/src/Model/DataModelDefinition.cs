using System.Collections.Generic;
using System.Reflection;

namespace ImpliciX.Language.Model
{
  public class DataModelDefinition
  {
    public Assembly Assembly { get; set; }
    public PropertyUrn<SoftwareVersion> AppVersion { get; set; }
    public PropertyUrn<Literal> AppEnvironment { get; set; }
    public (Urn, object)[] GlobalProperties { get; set; }
    public IDictionary<string, Urn> ModelBackwardCompatibility { get; set; }
  }
}