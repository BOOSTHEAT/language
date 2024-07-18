using System;
using ImpliciX.Language.Core;

namespace ImpliciX.Language.Model
{
    public interface IDataModelValue
    {
        TimeSpan At { get; }
        Urn Urn { get; }
        object ModelValue();
    }
    
    public interface IIMutableDataModelValue: IDataModelValue
    {
        IIMutableDataModelValue WithUrn(Urn urn);
        
        IIMutableDataModelValue WithAt(TimeSpan at);
        
        IIMutableDataModelValue With(Urn urn, TimeSpan at);
        
        
    }
    public delegate Result<IDataModelValue> ReadProperty(Urn urn);

   public static class ConverterExtensions
    {
        public static Result<float> ToFloat(this IDataModelValue property) =>
            property.ModelValue() switch
            {
                IFloat floatValue => floatValue.ToFloat(),
                Enum enumValue => Convert.ToSingle(enumValue),
                object x => Result<float>.Create(new Error(property.Urn, $"Unsupported conversion from {x.GetType()} to float" ))
            };

        public static ushort[] To16BitValue(this IDataModelValue property) =>
            property.ModelValue() switch
            {
                IFloat floatValue => ToUshort(floatValue.ToFloat()),
                Enum enumValue => ToUshort(Convert.ToInt16(enumValue)),
                _ => throw new NotImplementedException()
            };

        private static ushort[] ToUshort(float f)
        {
            var b = BitConverter.GetBytes(f);
            var msb = BitConverter.ToUInt16(b, 0);
            var lsb = BitConverter.ToUInt16(b, 2);
            return new[] {msb, lsb};
        }
        
        private static ushort[] ToUshort(short s)
        {
            var b = BitConverter.GetBytes(s);
            var word = BitConverter.ToUInt16(b, 0);
            return new[] {word};
        }
    }
}