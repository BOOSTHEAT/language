using System;
using System.IO;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct PackageContent : IEquatable<PackageContent>
    {
        public SoftwareDeviceNode DeviceNode { get; }
        public string Revision { get; }
        public byte[] Bytes => _bytes ?? System.IO.File.ReadAllBytes(ContentFile.FullName);

        private readonly byte[] _bytes;

        public FileInfo ContentFile { get; }

        [ModelFactoryMethod]
        public static Result<PackageContent> FromString(string value) => throw new NotImplementedException();

        public PackageContent(SoftwareDeviceNode deviceNode, string revision, FileInfo contentFile) :
            this(deviceNode, revision, default(byte[]))
        {
            ContentFile = contentFile;
        }

        public PackageContent(SoftwareDeviceNode deviceNode, string revision, byte[] bytes)
        {
            DeviceNode = deviceNode;
            Revision = revision;
            _bytes = bytes;
            ContentFile = default(FileInfo);
        }

        public override string ToString() =>
            $"{nameof(PackageContent)}: SoftwareDevice {DeviceNode.Urn} {Revision} size {_bytes?.Length}(bytes)";

        public bool Equals(PackageContent other)
        {
            return Equals(DeviceNode.Urn, other.DeviceNode.Urn)
                   && Revision == other.Revision &&
                   Equals(ContentFile?.Name, other.ContentFile?.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is PackageContent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeviceNode.Urn, Revision, ContentFile?.FullName);
        }

        public static bool operator ==(PackageContent left, PackageContent right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PackageContent left, PackageContent right)
        {
            return !left.Equals(right);
        }
    }
}