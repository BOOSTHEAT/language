using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Modbus
{
    [ValueObject]
    public struct FirstFrame
    {
        [ModelFactoryMethod]
        public static Result<FirstFrame> FromString(string str) => throw new NotSupportedException();

        public static FirstFrame Create(Partition partitionId, ulong sizeInBytes, string crc, string revision)
        {
            return new FirstFrame(partitionId, sizeInBytes, crc, revision);
        }

        public Partition PartitionId { get; }
        public ulong SizeInBytes { get; }
        public string Crc { get; }
        public SoftwareVersion Revision { get; }

        private FirstFrame(Partition partitionId, ulong sizeInBytes, string crc, string revision)
        {
            PartitionId = partitionId;
            SizeInBytes = sizeInBytes;
            Crc = crc;
            Revision = SoftwareVersion.FromString(revision).GetValueOrDefault();
        }


        public bool Equals(FirstFrame other)
        {
            return PartitionId == other.PartitionId && SizeInBytes == other.SizeInBytes && Crc == other.Crc && Revision.Equals(other.Revision);
        }

        public override bool Equals(object obj)
        {
            return obj is FirstFrame other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) PartitionId, SizeInBytes, Crc, Revision);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}