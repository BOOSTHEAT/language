using System;
using System.Linq;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.StdLib;

[TestFixture(typeof(Temperature))]
[TestFixture(typeof(Power))]
[TestFixture(typeof(Pressure))]
public class DebuggingDecodersTests<T> where T : IFloat<T>
{
  [TestCase(0, 0, 0f)]
  [TestCase(17086, 32768, 95.25f)]
  [TestCase(17563, 33792, 1244.125f)]
  [TestCase(16384, 17283, 2.00412059f)]
  [TestCase(13107, 17283, 4.17380868E-08f)]
  [TestCase(0, 17283, 2.42186414E-41f)]
  [TestCase( 17283,16384, 262.5f)]
  [TestCase( 17283,13107, 262.399994f)]
  [TestCase( 17283, 0, 262.0f)]
  public void ReadFloat(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.FloatMswFirst<T>(), word1, word2, expectedValue);
    Check(DebuggingDecoder.FloatMswLast<T>(), word2, word1, expectedValue);
  }
  
  [TestCase(0, 0, 10f)]
  [TestCase(17086, 32768, 200.5f)]
  [TestCase(17563, 33792, 2498.25f)]
  public void ReadFloatWithConversion(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.FloatMswFirst<T>(x => 2*x+10), word1, word2, expectedValue);
    Check(DebuggingDecoder.FloatMswLast<T>(x => 2*x+10), word2, word1, expectedValue);
  }

  [TestCase(0, 0, 0f)]
  [TestCase(17086, 32768, 1119780864f)]
  [TestCase(17563, 33792, 1151042560f)]
  public void Read32BitsUnsignedInt(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.UInt32MswFirst<T>(), word1, word2, expectedValue);
    Check(DebuggingDecoder.UInt32MswLast<T>(), word2, word1, expectedValue);
  }
  
  [TestCase(0, 0, 10f)]
  [TestCase(17086, 32768, 2239561738f)]
  [TestCase(17563, 33792, 2302085130f)]
  public void Read32BitsUnsignedIntWithConversion(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.UInt32MswFirst<T>(x => 2*x+10), word1, word2, expectedValue);
    Check(DebuggingDecoder.UInt32MswLast<T>(x => 2*x+10), word2, word1, expectedValue);
  }
  
  [TestCase(0, 0, 0f)]
  [TestCase(0, 42, 42f)]
  [TestCase(65535, 65535, -1f)]
  public void Read32BitsSignedInt(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.SInt32MswFirst<T>(), word1, word2, expectedValue);
    Check(DebuggingDecoder.SInt32MswLast<T>(), word2, word1, expectedValue);
  }
  
  [TestCase(0, 0, 10f)]
  [TestCase(0, 42, 94f)]
  [TestCase(65535, 65535, 8f)]
  public void Read32BitsSignedIntWithConversion(int word1, int word2, float expectedValue)
  {
    Check(DebuggingDecoder.SInt32MswFirst<T>(x => 2*x+10), word1, word2, expectedValue);
    Check(DebuggingDecoder.SInt32MswLast<T>(x => 2*x+10), word2, word1, expectedValue);
  }

  private static void Check(MeasureDecoder decoder, int word1, int word2, float expectedValue)
  {
    Check((measureUrn, statusUrn, time) => decoder.Invoke(
      measureUrn, statusUrn,
      new[] { (ushort)word1, (ushort)word2 },
      time, null
    ), expectedValue);
  }

  
  [TestCase(0,  0f)]
  [TestCase(17086,  17086f)]
  [TestCase(33792, 33792f)]
  public void Read16BitsUnsignedInt(int word, float expectedValue)
  {
    Check(DebuggingDecoder.UInt16<T>(), word, expectedValue);
  }
  
  [TestCase(0,  10f)]
  [TestCase(17086,  34182f)]
  [TestCase(33792, 67594f)]
  public void Read16BitsUnsignedIntWithConversion(int word, float expectedValue)
  {
    Check(DebuggingDecoder.UInt16<T>(x => 2*x+10), word, expectedValue);
  }

  [TestCase(0,  0f)]
  [TestCase(17086,  17086f)]
  [TestCase(33792, 33792f-65536f)]
  public void Read16BitsSignedInt(int word, float expectedValue)
  {
    Check(DebuggingDecoder.SInt16<T>(), word, expectedValue);
  }
  
  [TestCase(0,  10f)]
  [TestCase(17086,  34182f)]
  [TestCase(33792, 2*(33792f-65536f)+10)]
  public void Read16BitsSignedIntWithConversion(int word, float expectedValue)
  {
    Check(DebuggingDecoder.SInt16<T>(x => 2*x+10), word, expectedValue);
  }

  private static void Check(MeasureDecoder decoder, int word, float expectedValue)
  {
    Check((measureUrn, statusUrn, time) => decoder.Invoke(
      measureUrn, statusUrn,
      new[] { (ushort)word },
      time, null
    ), expectedValue);
  }

  private static void Check(
    Func<
      PropertyUrn<T>,
      PropertyUrn<MeasureStatus>,
      TimeSpan,
      ImpliciX.Language.Core.Result<IMeasure>
    > getValue,
    float expectedValue)
  {
    var measureUrn = PropertyUrn<T>.Build("measure");
    var statusUrn = PropertyUrn<MeasureStatus>.Build("status");
    var time = new TimeSpan(Random.Shared.Next());
    var result = getValue(measureUrn,statusUrn,time);
    Assert.True(result.IsSuccess, "shall always succeed");
    var measure = result.Value.ModelValues().ElementAt(0);
    Assert.That(measure.Urn, Is.EqualTo(measureUrn));
    Assert.That(((T)measure.ModelValue()).ToFloat(), Is.EqualTo(expectedValue));
    Assert.That(measure.At, Is.EqualTo(time));
    var status = result.Value.ModelValues().ElementAt(1);
    Assert.That(status.Urn, Is.EqualTo(statusUrn));
    Assert.That(status.ModelValue(), Is.EqualTo(MeasureStatus.Success));
    Assert.That(status.At, Is.EqualTo(time));
  }
  
  [TestCase(float.NaN)]
  [TestCase(float.PositiveInfinity)]
  [TestCase(float.NegativeInfinity)]
  public void ErroneousValuesAreMeasureError(float erroneousValue)
  {
    var result = DebuggingDecoder.CreateAsIFloat<T>(erroneousValue);
    Assert.True(result.IsError);
    Assert.That(result.Error.Message, Is.EqualTo($"INVALID_VALUE:Cannot create {typeof(T).Name} from value {erroneousValue}"));
  }

}