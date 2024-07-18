namespace ImpliciX.Language.Model
{
    public class ManualAlert<T> : SubSystemNode
    {
        public ManualAlert(string urnToken, ModelNode parent) : base(urnToken, parent)
        {
            _reset = CommandUrn<NoArg>.Build(Urn, "RESET");
            public_state = PropertyUrn<Alert>.Build(Urn, nameof(public_state));
            settings = new AlertSettings<T>(this);
            ready_to_reset = PropertyUrn<Reset>.Build(Urn, nameof(ready_to_reset));
        }

        public CommandUrn<NoArg> _reset { get; }
        public PropertyUrn<Alert> public_state { get; }
        public PropertyUrn<Reset> ready_to_reset { get; }
        public AlertSettings<T> settings { get; }
    }

    public class AlertSettings<T> : ModelNode
    {
        public AlertSettings(ModelNode parent) : base("settings", parent)
        {
            presence = VersionSettingUrn<Presence>.Build(Urn, nameof(presence));
            set_threshold = VersionSettingUrn<T>.Build(Urn, nameof(set_threshold));
            reset_threshold = VersionSettingUrn<T>.Build(Urn, nameof(reset_threshold));
            upper_threshold = VersionSettingUrn<T>.Build(Urn, nameof(upper_threshold));
            lower_threshold = VersionSettingUrn<T>.Build(Urn, nameof(lower_threshold));
            timeout = VersionSettingUrn<Duration>.Build(Urn, nameof(timeout));
            reset_timeout = VersionSettingUrn<Duration>.Build(Urn, nameof(reset_timeout));
        }

        public VersionSettingUrn<Presence> presence { get; }
        public VersionSettingUrn<T> set_threshold { get; }
        public VersionSettingUrn<T> reset_threshold { get; }
        public VersionSettingUrn<T> upper_threshold { get; }
        public VersionSettingUrn<T> lower_threshold { get; }
        public VersionSettingUrn<Duration> timeout { get; }
        public VersionSettingUrn<Duration> reset_timeout { get; }
    }
}