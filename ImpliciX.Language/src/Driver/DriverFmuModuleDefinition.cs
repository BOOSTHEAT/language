using System;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Driver
{
  public class DriverFmuModuleDefinition
  {
    public CommandUrn<NoArg> StartSimulation { get; set; }
    public CommandUrn<NoArg> StopSimulation { get; set; }
    public string FmuPackage { get; set; }
    public (string, string)[] ParameterFiles { get; set; } = Array.Empty<(string, string)>();
    public (Urn, Urn, string)[] ReadVariables { get; set; } = Array.Empty<(Urn, Urn, string)>();
    public (Urn, string)[] WriteVariables { get; set; } = Array.Empty<(Urn, string)>();
  }
}