using System;
using System.Collections.Generic;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus;

public static class RegistersMap
{
  public static IRegistersMap Create() => CreateRegistersMap();
  public static IRegistersMap Empty() => CreateRegistersMap();

  private static Func<IRegistersMap> CreateRegistersMap =>
    Factory ?? throw new Exception("RegistersMap factory was not defined");
  public static Func<IRegistersMap> Factory { private get; set; }
}

public interface IRegistersMap
{
  IConversionDefinition For<T>(MeasureNode<T> node);
  IEnumerable<IConversionDefinition> Conversions { get; }
  IRegistersMap RegistersSegmentsDefinitions(params RegistersSegmentsDefinition[] segDef);
  RegistersSegmentsDefinition[] SegmentsDefinition { get; }
}

public delegate Result<IMeasure> MeasureDecoder(Urn measureUrn, Urn measureStatus, ushort[] measureRegisters,
  TimeSpan currentTime, IDriverStateKeeper driverStateKeeper);

public struct Slice
{
  public Slice(ushort segmentIndex, ushort dataIndexInSegment, ushort startRegister, ushort registerCount)
  {
    SegmentIndex = segmentIndex;
    DataIndexInSegment = dataIndexInSegment;
    StartRegister = startRegister;
    RegisterCount = registerCount;
  }
  public ushort SegmentIndex { get; }
  public ushort DataIndexInSegment { get; }
  public ushort StartRegister { get; }
  public ushort RegisterCount { get; }
}

public interface IConversionDefinition
{
  Slice[] Slices { get; }
  Urn MeasureUrn { get; }
  Urn StatusUrn { get; }
  IRegistersMap DecodeRegisters((ushort startIndex, ushort count)[] slices, MeasureDecoder func);
  IRegistersMap DecodeRegisters(ushort startIndex, ushort count, MeasureDecoder func);
  Result<IMeasure> Decode(ushort[] measureRegisters, TimeSpan currentTime, IDriverStateKeeper driverStateKeeper);
}
  
