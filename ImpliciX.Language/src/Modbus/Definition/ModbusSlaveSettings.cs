namespace ImpliciX.Language.Modbus
{
    public class ModbusSlaveSettings
    {
        public string Factory { get; set; }
        public byte Id { get; set; }
        public TimeoutSettings TimeoutSettings { get; set; }
        public uint ReadPaceInSystemTicks { get; set; }
    }
    
    public class TimeoutSettings
    {
        public int Timeout { get; set; }
        public int Retries { get; set; }
    }
}