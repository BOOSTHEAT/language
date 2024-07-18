namespace ImpliciX.Language.Model
{
  public class BurnerNode : SubSystemNode
  {
    public BurnerNode(string urnToken, ModelNode parent) : base(urnToken, parent)
    {
      _supply = CommandUrn<PowerSupply>.Build(Urn, "SUPPLY");
      _start_ignition = CommandUrn<NoArg>.Build(Urn, "START_IGNITION");
      _stop_ignition = CommandUrn<NoArg>.Build(Urn, "STOP_IGNITION");
      _manual_reset = CommandUrn<NoArg>.Build(Urn, "MANUAL_RESET");
      status = PropertyUrn<GasBurnerStatus>.Build(Urn,nameof(status));
      ignition_fault = new MeasureNode<Fault>(nameof(ignition_fault), this);
      ignition_settings = new IgnitionSettingsNode(nameof(ignition_settings), this);
    }
    public FanNode burner_fan { get; set;}

    public CommandUrn<PowerSupply> _supply { get; }
    public CommandUrn<NoArg> _manual_reset { get; }
    public CommandUrn<NoArg> _start_ignition { get; }
    public CommandUrn<NoArg> _stop_ignition { get; }
    public PropertyUrn<GasBurnerStatus> status { get; }
    public MeasureNode<Fault> ignition_fault { get; }
    public IgnitionSettingsNode ignition_settings { get; }
    
    public class IgnitionSettingsNode : ModelNode
    {
      public IgnitionSettingsNode(string urnToken, ModelNode parent) : base(urnToken, parent)
      {
        ignition_period = VersionSettingUrn<Duration>.Build(Urn, nameof(ignition_period));
        ignition_supplying_delay = VersionSettingUrn<Duration>.Build(Urn, nameof(ignition_supplying_delay));
        ignition_reset_delay = VersionSettingUrn<Duration>.Build(Urn, nameof(ignition_reset_delay));
      }
      public VersionSettingUrn<Duration> ignition_period { get; }
      public VersionSettingUrn<Duration> ignition_supplying_delay {get;} 
      public VersionSettingUrn<Duration> ignition_reset_delay { get; }
    }
  }
}