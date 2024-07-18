using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Modbus
{
    public static class RegistersConverterHelper
    {
        public static float ToFloatMswFirst(ushort[] registers)
        {
            Debug.PreCondition(() => registers.Length == 2,
                () => $"2 Registers are required to extract a float (not {registers.Length})");
            var bytes = ConvertToByteArray(registers[1], registers[0]);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static float ToFloatMswLast(ushort[] registers)
        {
            Debug.PreCondition(() => registers.Length == 2,
                () => $"2 Registers are required to extract a float (not {registers.Length})");
            var bytes = ConvertToByteArray(registers[0], registers[1]);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static uint ToUnsignedIntMswFirst(ushort[] registers)
        {
            Debug.PreCondition(() => registers.Length == 2,
                () => $"2 Registers are required to extract an unsigned int (not {registers.Length})");
            var bytes = ConvertToByteArray(registers[1], registers[0]);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static uint ToUnsignedIntMswLast(ushort[] registers)
        {
            Debug.PreCondition(() => registers.Length == 2,
                () => $"2 Registers are required to extract an unsigned int (not {registers.Length})");
            var bytes = ConvertToByteArray(registers[0], registers[1]);
            return BitConverter.ToUInt32(bytes, 0);
        }

        private static byte[] ConvertToByteArray(ushort lsw, ushort msw)
        {
            var mswBytes = BitConverter.GetBytes(msw);
            var lswBytes = BitConverter.GetBytes(lsw);
            return new[] {lswBytes[0], lswBytes[1], mswBytes[0], mswBytes[1]};
        }

        public static ushort[] ToRegisters(float f)
        {
            var b = BitConverter.GetBytes(f);
            var msb = BitConverter.ToUInt16(b, 0);
            var lsb = BitConverter.ToUInt16(b, 2);
            return new[] { msb, lsb };
        }

        public static short ToShort(ushort[] registers)
        {
            Debug.PreCondition(() => registers.Length == 1, () => "1 Register is required to extract a short");
            return BitConverter.ToInt16(BitConverter.GetBytes(registers[0]), 0);
        }

        public static ushort[] ToRegisters(short s)
        {
            var b = BitConverter.GetBytes(s);
            var word = BitConverter.ToUInt16(b, 0);
            return new[] { word };
        }

        public static ushort[] ToRegisters(byte[] bytes)
        {
            var registers = new List<ushort>();
            var length = (bytes.Length % 2 == 0) ? bytes.Length : bytes.Length - 1;

            for (var n = 0; n < length; n += 2)
            {
                var result = (ushort)(((bytes[n + 1] & 0xff) << 8) | (ushort)((bytes[n] & 0xff)));
                registers.Add(result);
            }

            if (bytes.Length % 2 != 0)
            {
                var result = (ushort)(bytes[^1] & 0xff);
                registers.Add(result);
            }

            return registers.ToArray();
        }
    }
}