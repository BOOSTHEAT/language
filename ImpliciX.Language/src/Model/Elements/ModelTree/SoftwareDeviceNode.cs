namespace ImpliciX.Language.Model
{
    public class SoftwareDeviceNode : DeviceNode
    {
        public SoftwareDeviceNode(string name, ModelNode parent) : base(name, parent)
        {
            _update = CommandNode<PackageContent>.Create("UPDATE", this);
            software_version = new MeasureNode<SoftwareVersion>(nameof(software_version), this);
            fallback_version = new MeasureNode<SoftwareVersion>(nameof(fallback_version), this);
            update_progress = PropertyUrn<Percentage>.Build(Urn, nameof(update_progress));
            update_state = PropertyUrn<UpdateState>.Build(Urn, nameof(update_state));
        }

        public MeasureNode<SoftwareVersion> software_version { get; }

        public MeasureNode<SoftwareVersion> fallback_version { get; }
        public CommandNode<PackageContent> _update { get; }
        public PropertyUrn<Percentage> update_progress { get; }
        public PropertyUrn<UpdateState> update_state { get; }
    }
}