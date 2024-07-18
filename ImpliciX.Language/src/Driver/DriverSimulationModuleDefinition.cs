using System;
using System.Collections.Generic;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Driver
{
  public class DriverSimulationModuleDefinition
  {
    public Func<IPropertySimulation,Func<TimeSpan, IEnumerable<IDataModelValue>>[]> Properties { get; set; }
  }
}