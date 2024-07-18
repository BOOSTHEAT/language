using ImpliciX.Language.Driver;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus
{
    public class BrahmaSlaveDefinition:ModbusSlaveDefinition
    {
        public BurnerNode GenericBurner { get; set; }

        public BrahmaSlaveDefinition(DeviceNode deviceNode, BurnerNode genericBurner) : base(deviceNode, SlaveKind.Brahma)
        {
            GenericBurner = genericBurner;
        }
    }
}