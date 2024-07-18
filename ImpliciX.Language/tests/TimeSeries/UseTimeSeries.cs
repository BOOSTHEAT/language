using System;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.TimeSeries;

public class UseTimeSeries
{
  [Test]
  public void InFrozenTimeSeries()
  {
    var def = new FrozenTimeSeriesDefinition
    {
      TimeSeries = new [] {
        MySlave.Definition.TimeSeries()
      }
    };
    Assert.That(def.TimeSeries[0] is ModbusSlaveTimeSeries);
  }

  [Test]
  public void InHttpTimeSeries()
  {
    var def = new HttpTimeSeriesDefinition
    {
      TimeSeries = new [] {
        MySlave.Definition.TimeSeries().Over.ThePast(5).Days
      }
    };
    Assert.That(def.TimeSeries[0].Definition is ModbusSlaveTimeSeries);
    Assert.That(def.TimeSeries[0].TimeSpan, Is.EqualTo(TimeSpan.FromDays(5)));
  }

  static class MySlave
  {
    private static readonly HardwareDeviceNode HwDeviceNode = new("my_device", new RootModelNode("root"));

    public static readonly Func<ModbusSlaveDefinition> Definition = () => new(HwDeviceNode)
    {
      Name = nameof(MySlave),
      SettingsUrns = new Urn[]{ HwDeviceNode.presence }
    };
  }
}