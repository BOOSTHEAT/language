using ImpliciX.Language.Model;

namespace ImpliciX.Language.Alarms
{
    public readonly struct Alarm
    {
        public enum AlarmKind
        {
            Auto,
            Communication,
            Manual,
            Trigger,
            Measure
        }

        public static Alarm Auto(AlarmNode alarmNode, Urn publicState) =>
            new Alarm(alarmNode, AlarmKind.Auto, publicState);

        public static Alarm Communication(AlarmNode alarmNode, Urn boardUrn) =>
            new Alarm(alarmNode, AlarmKind.Communication, boardUrn);

        public static Alarm Manual(AlarmNode alarmNode, Urn publicState, Urn readyToReset, Urn resetCommand) =>
            new Alarm(alarmNode, AlarmKind.Manual, publicState, readyToReset, resetCommand);

        public static Alarm Trigger(AlarmNode alarmNode, Triggers trans) =>
            new Alarm(alarmNode, AlarmKind.Trigger, trans);

        public static Alarm Measure(AlarmNode alarmNode, Urn measureUrn) =>
            new Alarm(alarmNode, AlarmKind.Measure, measureUrn);

        private Alarm(AlarmNode alarmNode, AlarmKind kind, params Urn[] dependencies)
        {
            Node = alarmNode;
            Kind = kind;
            Dependencies = dependencies;
            Triggers = new Triggers();
        }
        
        private Alarm(AlarmNode alarmNode, AlarmKind kind, Triggers triggers)
        {
            Node = alarmNode;
            Kind = kind;
            Dependencies = new Urn[0];
            Triggers = triggers;
        }
        
        public readonly AlarmNode Node;
        public readonly AlarmKind Kind;
        public readonly Urn[] Dependencies;
        public readonly Triggers Triggers;
    }
}