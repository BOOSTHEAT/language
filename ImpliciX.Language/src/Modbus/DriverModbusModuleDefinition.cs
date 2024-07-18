using System;
using ImpliciX.Language.Driver;

namespace ImpliciX.Language.Modbus
{
  public class DriverModbusModuleDefinition
  {
    public ModbusSlaveModel ModbusSlavesManagement { get; set; }
    public Func<ModbusSlaveDefinition>[] Slaves { get; set; }
  }
}