using ImpliciX.Language.Model;

namespace ImpliciX.Language
{
  public class MotorsModuleDefinition
  {
    public MotorsModuleDefinition()
    {
      MotorNodes = new MotorNode[]{};
    }
    public HardwareAndSoftwareDeviceNode MotorsDeviceNode { get; set; }
    public DeviceNode HeatPumpDeviceNode { get; set; }
    public MotorNode[] MotorNodes { get; set; }
    public PropertyUrn<Duration> SettingsSupplyDelayTimerUrn { get; set; }
    public PropertyUrn<MotorsStatus> MotorsStatusUrn { get; set; }
    public CommandNode<PowerSupply> SupplyCommand { get; set; }
    public CommandNode<PowerSupply> PowerCommand { get; set; }
    public CommandUrn<MotorStates> SwitchCommandUrn { get; set; }
  }
}