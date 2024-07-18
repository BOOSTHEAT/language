namespace ImpliciX.Language.Model
{
    public interface IHardwareDevice
    {
        Urn Urn { get; }
        UserSettingUrn<Presence> presence { get; }
    }
    
    public class HardwareDeviceNode : DeviceNode, IHardwareDevice 
    {
        public HardwareDeviceNode(string name, ModelNode parent) : base(name, parent)
        {
            presence = UserSettingUrn<Presence>.Build(Urn, nameof(presence));
            serial_number = VersionSettingUrn<Literal>.Build(Urn, nameof(serial_number));
        }

        public UserSettingUrn<Presence> presence { get; }
        public VersionSettingUrn<Literal> serial_number { get; }
    }
}