#nullable enable
using System;
using System.Linq;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus
{
    [ValueObject]
    public struct Chunk
    {
        public float Progress { get; }

        [ModelFactoryMethod]
        public static Result<Chunk> FromString(string str) => throw new NotSupportedException();

        public static Chunk Create(ushort[] bytes, int index, int max) => new Chunk(bytes,index, max);

        public int Index { get; }

        public ushort[] Registers { get; }

        private Chunk(ushort[] registers, int index, int max)
        {
            Progress = (float)(index + 1) / max;
            Index = index;
            Registers = registers;
        }

        public bool IsLast() => Math.Abs(Progress - 1f) < float.Epsilon;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            var chunk = (Chunk) obj;
            return chunk.Registers.SequenceEqual(Registers) && Index == chunk.Index && MathF.Abs(Progress - chunk.Progress) < float.Epsilon;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Registers, Index, Progress);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public (int id, ushort[] registers) Content() => (Index, Registers);
    }
}