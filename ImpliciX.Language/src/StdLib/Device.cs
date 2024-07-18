using ImpliciX.Language.Model;

namespace ImpliciX.Language.StdLib;

public class Device : RootModelNode
{
  public Device(string name) : base(name)
  {
    app = new SoftwareDeviceNode(nameof(app), this);
    gui = new SoftwareDeviceNode(nameof(gui), this);
    bsp = new SoftwareDeviceNode(nameof(bsp), this);
    _reboot = CommandUrn<NoArg>.Build(Urn, "REBOOT");
    _restart = CommandUrn<NoArg>.Build(Urn, "RESTART");
    _clean_version_settings = CommandNode<NoArg>.Create("CLEAN_VERSION_SETTINGS", this);
    environment = PropertyUrn<Literal>.Build(Urn, nameof(environment));
    serial_number = FactorySettingUrn<Literal>.Build(Urn, nameof(serial_number));
    locale = UserSettingUrn<Locale>.Build(Urn, nameof(locale));
    timezone = UserSettingUrn<TimeZone>.Build(Urn, nameof(timezone));
    software = new Software(this);
  }

  public SoftwareDeviceNode app { get; }
  public SoftwareDeviceNode gui { get; }
  public SoftwareDeviceNode bsp { get; }
  public CommandUrn<NoArg> _reboot { get; }
  public CommandUrn<NoArg> _restart { get; }
  public CommandNode<NoArg> _clean_version_settings { get; }
  public PropertyUrn<Literal> environment { get; }
  public UserSettingUrn<Locale> locale { get; }
  public UserSettingUrn<TimeZone> timezone { get; }
  public FactorySettingUrn<Literal> serial_number { get; }
  public Software software { get; }

  public class Software : ModelNode
  {
    public CommandNode<NoArg> _commit_update { get; }
    public CommandNode<PackageLocation> _update { get; }
    public CommandNode<NoArg> _rollback_update { get; }
    public PropertyUrn<UpdateState> update_state { get; }
    public MeasureNode<SoftwareVersion> version { get; }

    public Software(ModelNode parent) : base(nameof(software), parent)
    {
      _update = CommandNode<PackageLocation>.Create("UPDATE", this);
      _commit_update = CommandNode<NoArg>.Create("COMMIT_UPDATE", this);
      _rollback_update = CommandNode<NoArg>.Create("ROLLBACK_UPDATE", this);
      update_state = PropertyUrn<UpdateState>.Build(Urn, nameof(update_state));
      version = new MeasureNode<SoftwareVersion>(nameof(version), this);
    }
  }
}
