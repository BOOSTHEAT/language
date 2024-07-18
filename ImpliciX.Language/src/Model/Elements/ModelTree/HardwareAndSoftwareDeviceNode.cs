namespace ImpliciX.Language.Model
{
    public class HardwareAndSoftwareDeviceNode : SoftwareDeviceNode, IHardwareDevice
    {
        public HardwareAndSoftwareDeviceNode(string name, ModelNode parent) : base(name, parent)
        {
            presence = UserSettingUrn<Presence>.Build(Urn, nameof(presence));
            serial_number = FactorySettingUrn<Literal>.Build(Urn, nameof(serial_number));
            bootloader_version = new MeasureNode<SoftwareVersion>(nameof(bootloader_version), this);
            last_error = new MeasureNode<LogicalDeviceError>(nameof(last_error), this);
            rssi = new MeasureNode<Power>(nameof(rssi), this);
        }

        public UserSettingUrn<Presence> presence { get; }
        public FactorySettingUrn<Literal> serial_number { get; }
        public MeasureNode<SoftwareVersion> bootloader_version { get;}
        public MeasureNode<LogicalDeviceError> last_error { get; }
        public MeasureNode<Power> rssi { get; }
    }
}