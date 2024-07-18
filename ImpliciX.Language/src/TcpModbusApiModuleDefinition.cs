using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
  public class TcpModbusApiModuleDefinition
  {
    public PropertyUrn<Presence> Presence { get; set; }
    public Dictionary<Urn, ushort> AlarmsMap { get; set; }
    public Dictionary<Urn, ushort> MeasuresMap { get; set; }
  }
}