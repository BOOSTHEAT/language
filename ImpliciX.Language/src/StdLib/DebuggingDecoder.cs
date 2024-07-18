using System;
using System.Reflection;
using ImpliciX.Language.Core;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Modbus.RegistersConverterHelper;

namespace ImpliciX.Language.StdLib;

public static class DebuggingDecoder
{
  public static MeasureDecoder FloatMswFirst<T>() where T : IFloat => FloatMswFirst<T>(x => x);
  public static MeasureDecoder FloatMswLast<T>() where T : IFloat => FloatMswLast<T>(x => x);
  public static MeasureDecoder UInt32MswFirst<T>() where T : IFloat => UInt32MswFirst<T>(x => x);
  public static MeasureDecoder UInt32MswLast<T>() where T : IFloat => UInt32MswLast<T>(x => x);
  public static MeasureDecoder SInt32MswFirst<T>() where T : IFloat => SInt32MswFirst<T>(x => x);
  public static MeasureDecoder SInt32MswLast<T>() where T : IFloat => SInt32MswLast<T>(x => x);
  public static MeasureDecoder UInt16<T>() where T : IFloat => UInt16<T>(x => x);
  public static MeasureDecoder SInt16<T>() where T : IFloat => SInt16<T>(x => x);

  public static MeasureDecoder FloatMswFirst<T>(Func<float, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter(ToFloatMswFirst(x)));

  public static MeasureDecoder FloatMswLast<T>(Func<float, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter(ToFloatMswLast(x)));

  public static MeasureDecoder UInt32MswFirst<T>(Func<uint, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter(ToUnsignedIntMswFirst(x)));

  public static MeasureDecoder UInt32MswLast<T>(Func<uint, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter(ToUnsignedIntMswLast(x)));

  public static MeasureDecoder SInt32MswFirst<T>(Func<int, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter((int)ToUnsignedIntMswFirst(x)));

  public static MeasureDecoder SInt32MswLast<T>(Func<int, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter((int)ToUnsignedIntMswLast(x)));

  public static MeasureDecoder UInt16<T>(Func<ushort, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter(x[0]));

  public static MeasureDecoder SInt16<T>(Func<short, float> converter) where T : IFloat =>
    CreateMeasure<T>(x => converter((short)x[0]));
  
  private static MeasureDecoder CreateMeasure<TModel>(Func<ushort[], float> converter) where TModel : IFloat =>
    (measureUrn, statusUrn, registers, currentTime, _) =>
      Measure<TModel>.Create(
        measureUrn, statusUrn,
        CreateAsIFloat<TModel>(converter(registers)),
        currentTime
      );

  internal static Result<TModel> CreateAsIFloat<TModel>(float f) where TModel : IFloat => float.IsFinite(f)
    ? CreateFromValidFloat<TModel>(f)
    : new Error("INVALID_VALUE", $"Cannot create {typeof(TModel).Name} from value {f}");

  private static Result<TModel> CreateFromValidFloat<TModel>(float f) where TModel : IFloat =>
    (Result<TModel>)typeof(TModel)
      .GetMethod("FromFloat", BindingFlags.Public | BindingFlags.Static)!
      .Invoke(null, new object[] { f })!;

  public static MeasureDecoder UInt32MswFirstEnum<T>() where T : Enum => UInt32MswFirstEnum<T>(x => x);
  public static MeasureDecoder UInt32MswLastEnum<T>() where T : Enum => UInt32MswLastEnum<T>(x => x);
  public static MeasureDecoder SInt32MswFirstEnum<T>() where T : Enum => SInt32MswFirstEnum<T>(x => x);
  public static MeasureDecoder SInt32MswLastEnum<T>() where T : Enum => SInt32MswLastEnum<T>(x => x);
  public static MeasureDecoder UInt16Enum<T>() where T : Enum => UInt16Enum<T>(x => x);
  public static MeasureDecoder SInt16Enum<T>() where T : Enum => SInt16Enum<T>(x => x);
  
  public static MeasureDecoder UInt32MswFirstEnum<T>(Func<uint, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter(ToUnsignedIntMswFirst(x)));

  public static MeasureDecoder UInt32MswLastEnum<T>(Func<uint, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter(ToUnsignedIntMswLast(x)));

  public static MeasureDecoder SInt32MswFirstEnum<T>(Func<int, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter((int)ToUnsignedIntMswFirst(x)));

  public static MeasureDecoder SInt32MswLastEnum<T>(Func<int, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter((int)ToUnsignedIntMswLast(x)));

  public static MeasureDecoder UInt16Enum<T>(Func<ushort, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter(x[0]));

  public static MeasureDecoder SInt16Enum<T>(Func<short, long> converter) where T : Enum =>
    CreateEnumMeasure<T>(x => converter((short)x[0]));

  private static MeasureDecoder CreateEnumMeasure<TModel>(Func<ushort[], long> converter) where TModel : Enum =>
    (measureUrn, statusUrn, registers, currentTime, _) =>
      Measure<TModel>.Create(
        measureUrn, statusUrn,
        CreateAsEnum<TModel>(converter(registers)),
        currentTime
      );

  private static Result<TModel> CreateAsEnum<TModel>(long l) where TModel : Enum
  {
    try
    {
      return (TModel)Enum.ToObject(typeof(TModel), l);
    }
    catch (Exception e)
    {
      return new Error("INVALID_VALUE", $"Cannot create {typeof(TModel).Name} from value {l}");
    }
  }
}