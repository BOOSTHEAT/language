namespace ImpliciX.Language.Model
{
  public class MotorNode : SubSystemNode
  {
    public MotorNode(string urnToken, ModelNode parent) : base(urnToken, parent)
    {
      _setpoint = CommandNode<RotationalSpeed>.Create("SETPOINT", this);
      mean_speed = new MeasureNode<RotationalSpeed>(nameof(mean_speed), this);
      temperature = new MeasureNode<Temperature>(nameof(temperature), this);
      mechanical_power = new MeasureNode<Power>(nameof(mechanical_power), this);
      current = new MeasureNode<Current>(nameof(current), this);
      supply_voltage = new MeasureNode<Voltage>(nameof(supply_voltage), this);
      voltage_operational_state = new MeasureNode<MotorVoltage>(nameof(voltage_operational_state), this);
      current_operational_state = new MeasureNode<MotorCurrent>(nameof(current_operational_state), this);
    }
    public CommandNode<RotationalSpeed> _setpoint { get; }
    public MeasureNode<RotationalSpeed> mean_speed { get; }
    public MeasureNode<Temperature> temperature { get; }
    public MeasureNode<Power> mechanical_power { get; }
    public MeasureNode<Current> current { get; }
    public MeasureNode<Voltage> supply_voltage { get; }
    public MeasureNode<MotorVoltage> voltage_operational_state { get; }
    public MeasureNode<MotorCurrent> current_operational_state { get; }
  }
  
}