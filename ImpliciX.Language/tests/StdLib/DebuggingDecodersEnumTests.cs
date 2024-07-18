using System;
using System.Linq;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.StdLib;

public enum MyEnum
{
  A = 0,
  B = 1,
  C = 42,
  D = 1119780864,
  E = -1
}

public class DebuggingDecodersEnumTests
{
  [TestCase(0, 0, MyEnum.A)]
  [TestCase(0, 1, MyEnum.B)]
  [TestCase(0, 42, MyEnum.C)]
  [TestCase(17086, 32768, MyEnum.D)]
  [TestCase(65535, 65535, MyEnum.E)]
  public void Read32BitsUnsignedInt(int word1, int word2, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.UInt32MswFirstEnum<MyEnum>(), word1, word2, expectedValue);
    Check(DebuggingDecoder.UInt32MswLastEnum<MyEnum>(), word2, word1, expectedValue);
  }

  [TestCase(0, 1, MyEnum.A)]
  [TestCase(0, 2, MyEnum.B)]
  [TestCase(0, 43, MyEnum.C)]
  [TestCase(17086, 32769, MyEnum.D)]
  [TestCase(0, 0, MyEnum.E)]
  public void Read32BitsUnsignedIntWithConversion(int word1, int word2, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.UInt32MswFirstEnum<MyEnum>(x => x - 1), word1, word2, expectedValue);
    Check(DebuggingDecoder.UInt32MswLastEnum<MyEnum>(x => x - 1), word2, word1, expectedValue);
  }

  [TestCase(0, 0, MyEnum.A)]
  [TestCase(0, 1, MyEnum.B)]
  [TestCase(0, 42, MyEnum.C)]
  [TestCase(17086, 32768, MyEnum.D)]
  [TestCase(65535, 65535, MyEnum.E)]
  public void Read32BitsSignedInt(int word1, int word2, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.SInt32MswFirstEnum<MyEnum>(), word1, word2, expectedValue);
    Check(DebuggingDecoder.SInt32MswLastEnum<MyEnum>(), word2, word1, expectedValue);
  }

  [TestCase(0, 1, MyEnum.A)]
  [TestCase(0, 2, MyEnum.B)]
  [TestCase(0, 43, MyEnum.C)]
  [TestCase(17086, 32769, MyEnum.D)]
  [TestCase(0, 0, MyEnum.E)]
  public void Read32BitsSignedIntWithConversion(int word1, int word2, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.SInt32MswFirstEnum<MyEnum>(x => x - 1), word1, word2, expectedValue);
    Check(DebuggingDecoder.SInt32MswLastEnum<MyEnum>(x => x - 1), word2, word1, expectedValue);
  }

  private static void Check(MeasureDecoder decoder, int word1, int word2, MyEnum expectedValue)
  {
    Check((measureUrn, statusUrn, time) => decoder.Invoke(
      measureUrn, statusUrn,
      new[] { (ushort)word1, (ushort)word2 },
      time, null
    ), expectedValue);
  }


  [TestCase(0, MyEnum.A)]
  [TestCase(1, MyEnum.B)]
  [TestCase(42, MyEnum.C)]
  public void Read16BitsUnsignedInt(int word, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.UInt16Enum<MyEnum>(), word, expectedValue);
  }

  [TestCase(1, MyEnum.A)]
  [TestCase(2, MyEnum.B)]
  [TestCase(43, MyEnum.C)]
  [TestCase(0, MyEnum.E)]
  public void Read16BitsUnsignedIntWithConversion(int word, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.UInt16Enum<MyEnum>(x => x - 1), word, expectedValue);
  }

  [TestCase(0, MyEnum.A)]
  [TestCase(1, MyEnum.B)]
  [TestCase(42, MyEnum.C)]
  [TestCase(65535, MyEnum.E)]
  public void Read16BitsSignedInt(int word, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.SInt16Enum<MyEnum>(), word, expectedValue);
  }

  [TestCase(1, MyEnum.A)]
  [TestCase(2, MyEnum.B)]
  [TestCase(43, MyEnum.C)]
  [TestCase(0, MyEnum.E)]
  public void Read16BitsSignedIntWithConversion(int word, MyEnum expectedValue)
  {
    Check(DebuggingDecoder.SInt16Enum<MyEnum>(x => x - 1), word, expectedValue);
  }

  private static void Check(MeasureDecoder decoder, int word, MyEnum expectedValue)
  {
    Check((measureUrn, statusUrn, time) => decoder.Invoke(
      measureUrn, statusUrn,
      new[] { (ushort)word },
      time, null
    ), expectedValue);
  }

  private static void Check<T>(
    Func<
      PropertyUrn<T>,
      PropertyUrn<MeasureStatus>,
      TimeSpan,
      ImpliciX.Language.Core.Result<IMeasure>
    > getValue,
    T expectedValue)
  {
    var measureUrn = PropertyUrn<T>.Build("measure");
    var statusUrn = PropertyUrn<MeasureStatus>.Build("status");
    var time = new TimeSpan(Random.Shared.Next());
    var result = getValue(measureUrn, statusUrn, time);
    Assert.True(result.IsSuccess, "shall always succeed");
    var measure = result.Value.ModelValues().ElementAt(0);
    Assert.That(measure.Urn, Is.EqualTo(measureUrn));
    Assert.That((T)measure.ModelValue(), Is.EqualTo(expectedValue));
    Assert.That(measure.At, Is.EqualTo(time));
    var status = result.Value.ModelValues().ElementAt(1);
    Assert.That(status.Urn, Is.EqualTo(statusUrn));
    Assert.That(status.ModelValue(), Is.EqualTo(MeasureStatus.Success));
    Assert.That(status.At, Is.EqualTo(time));
  }
}