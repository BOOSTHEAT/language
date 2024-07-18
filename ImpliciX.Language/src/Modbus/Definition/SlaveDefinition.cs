using System;
using System.Collections.Generic;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Modbus;

public class ModbusSlaveDefinition
{
    public SlaveKind SlaveKind { get; }
    public DeviceNode DeviceNode { get; } 
    public required string Name { get; set; }
    public required Urn[] SettingsUrns { get; init;}
    public Dictionary<MapKind, IRegistersMap> ReadPropertiesMaps { get; init; }
    public ICommandMap CommandMap { get; init; }
    
    public ModbusSlaveDefinition(DeviceNode deviceNode, SlaveKind slaveKind = SlaveKind.Vendor)
    {
      SlaveKind = slaveKind;
      DeviceNode = deviceNode;
      ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>();
      CommandMap = Modbus.CommandMap.Empty();
    }
}

public static class ModbusSlaveExtensions
{
  public static ITimeSeries TimeSeries(this Func<ModbusSlaveDefinition> getDefinition)
  {
    return new ModbusSlaveTimeSeries(getDefinition);
  }
}

public class ModbusSlaveTimeSeries : ITimeSeries
{
  public Func<ModbusSlaveDefinition> GetDefinition { get; }

  public ModbusSlaveTimeSeries(Func<ModbusSlaveDefinition> getDefinition)
  {
    GetDefinition = getDefinition;
  }
}
