namespace ImpliciX.Language.Modbus
{
    public enum RegisterKind
    {
        Input,
        Holding
    }
    public class RegistersSegmentsDefinition
    {
        public RegistersSegmentsDefinition(RegisterKind kind)
        {
            Kind = kind;
        }

        public RegisterKind Kind { get; }
        public ushort StartAddress { get; set; }
        public ushort RegistersToRead { get; set; }
    }
}