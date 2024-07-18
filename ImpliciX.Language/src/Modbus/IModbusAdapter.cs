namespace ImpliciX.Language.Modbus
{
    public interface IModbusAdapter
    {
        ushort[] ReadRegisters(string factoryName, RegisterKind kind, ushort startAddress, ushort registersToRead);
        void WriteRegisters(string factoryName, ushort startAddress, ushort[] registersToWrite);
    }
}