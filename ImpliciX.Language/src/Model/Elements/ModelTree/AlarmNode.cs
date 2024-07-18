using System;
using System.Text.RegularExpressions;

namespace ImpliciX.Language.Model
{
    public class AlarmNode : ModelNode
    {
        private static readonly Regex AlarmCodeRegExp = new Regex(@"C(\d{3})\b");

        public AlarmNode(string name, ModelNode parent) : base(name, parent)
        {
            _reset = CommandUrn<NoArg>.Build(Urn, "RESET");
            state = PropertyUrn<AlarmState>.Build(Urn, nameof(state));
            ready_to_reset = PropertyUrn<AlarmReset>.Build(Urn, nameof(ready_to_reset));
            settings = new AlarmSettings(this);
            number = ExtractNumber(name, AlarmCodeRegExp);
        }

        public AlarmSettings settings { get; }
        public PropertyUrn<AlarmState> state { get; }
        public PropertyUrn<AlarmReset> ready_to_reset { get; }
        public CommandUrn<NoArg> _reset { get; }
        public ushort number { get; }
        private static ushort ExtractNumber(string token, Regex regExp)
        {
            var m = regExp.Match(token);
            if (!m.Success) throw new ArgumentException("Alarm node urn token should have a CXXX format. By example C001; C002...");
            return ushort.Parse(m.Groups[1].Value);
        }
    }

    public class AlarmSettings : ModelNode
    {
        public AlarmSettings(ModelNode parent) : base("settings", parent)
        {
            presence = VersionSettingUrn<Presence>.Build(Urn, nameof(presence));
        }

        public VersionSettingUrn<Presence> presence { get; }
    }
}