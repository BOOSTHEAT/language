using System;
using System.Text.RegularExpressions;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    [ValueObject]
    public readonly struct SoftwareVersion: IPublicValue
    {
        private const string StrictPattern = @"^(\d+)\.(\d+)\.(\d+)\.(\d+)$";
        private const string WildcardPattern = @"^(?:\*|\d+\.(?:\*|\d+\.(?:\*|\d+\.(?:\*|\d+))))$";

        [ModelFactoryMethod]
        public static Result<SoftwareVersion> FromString(string value)
        {
            var match = Regex.Match(value, StrictPattern);
            if (match.Success)
            {
                return new SoftwareVersion(
                    ushort.Parse(match.Groups[1].Value), 
                    ushort.Parse(match.Groups[2].Value),
                    ushort.Parse(match.Groups[3].Value), 
                    ushort.Parse(match.Groups[4].Value));
            }
            else
            {
                return new InvalidValueError($"{value} is not valid for {nameof(SoftwareVersion)}");
            }
        }

        public static bool IsInvalid(string value) => !Regex.Match(value, WildcardPattern).Success;


        public static SoftwareVersion Create(ushort major, ushort minor, ushort build, ushort revision)
        {
            return new SoftwareVersion(major, minor, build, revision);
        }

        private readonly ushort _major;
        private readonly ushort _minor;
        private readonly ushort _build;
        private readonly ushort _revision;

        private SoftwareVersion(ushort major, ushort minor, ushort build, ushort revision)
        {
            _major = major;
            _minor = minor;
            _build = build;
            _revision = revision;
        }

        public ushort Major => _major;

        public ushort Minor => _minor;

        public ushort Build => _build;

        public ushort Revision => _revision;

        public override string ToString()
        {
            return $"{_major}.{_minor}.{_build}.{_revision}";
        }

        public object PublicValue()
        {
            return ToString();
        }

        public bool Equals(SoftwareVersion other)
        {
            return _major == other._major && _minor == other._minor && _build == other._build &&
                   _revision == other._revision;
        }

        public override bool Equals(object obj)
        {
            return obj is SoftwareVersion other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_major, _minor, _build, _revision);
        }
    }
}