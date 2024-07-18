using NUnit.Framework;
using static ImpliciX.Language.Modbus.RegistersConverterHelper;

namespace ImpliciX.Language.Tests.Modbus
{
    [TestFixture]
    public class ConvertFromRegistersTest
    {
        [TestCase(new ushort[] {0, 16480 },3.5f)]
        [TestCase(new ushort[] { 0, 49248 }, -3.5f)]
        [TestCase(new ushort[] {56443, 17564}, 1254.89f)]
        [TestCase(new ushort[] {0X8000, 0x43EA}, 469f)]
        public void extract_float_from_registers(ushort[] registers, float extractedValue)
        {
            var value = ToFloatMswLast(registers);
            Assert.AreEqual(extractedValue,value);
        }
        
        [TestCase(new ushort[] {0x8000}, short.MinValue)]
        [TestCase(new ushort[] {0x7FFF}, short.MaxValue)]
        [TestCase(new ushort[] {0xFFD6}, -42)]
        [TestCase(new ushort[] {0x002A}, 42)]
        [TestCase(new ushort[] {0}, 0)]
        public void extract_short_from_registers(ushort[] registers, short extractedValue)
        {
            var value = ToShort(registers);
            Assert.AreEqual(extractedValue,value);
        }

        [TestCase(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06}, new ushort[]{0x0201, 0x0403, 0x0605})]
        [TestCase(new byte[] {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07}, new ushort[]{0x0201, 0x0403, 0x0605, 0x0007})]
        public void extract_registers_from_bytes(byte[] bytes, ushort[] expected)
        {
            var values = ToRegisters(bytes);
            Assert.AreEqual(expected , values);
        }
    }
}