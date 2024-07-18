using System;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model
{
  public class ConversionTests
  {
    [TestCase(typeof(DummyFloat), 2568.0f, new ushort[] { 32768, 17696 })]
    [TestCase(typeof(DummyEnum), DummyEnum.C, new ushort[] { 65535 })]
    [TestCase(typeof(MetricValue), 123.45f, new ushort[] { 58982, 17142 })]
    public void convert_to_16bits(Type type, object value, ushort[] expected)
    {
      Check.That(CreateDataModelValue(type, value).To16BitValue()).IsEqualTo(expected);
    }

    [TestCase(typeof(DummyFloat), 2568.0f, 2568.0f)]
    [TestCase(typeof(DummyEnum), DummyEnum.C, -1)]
    [TestCase(typeof(MetricValue), 123.45f, 123.45f)]
    public void convert_to_float(Type type, object value, float expected)
    {
      Check.That(CreateDataModelValue(type, value).ToFloat().Value).IsEqualTo(expected);
    }


    private IDataModelValue CreateDataModelValue(Type type, object value)
    {
      if (type == typeof(DummyFloat))
        return Property<DummyFloat>.Create(PropertyUrn<DummyFloat>.Build("foo"), new DummyFloat((float)value),
          TimeSpan.Zero);

      if (type == typeof(DummyEnum))
        return Property<DummyEnum>.Create(PropertyUrn<DummyEnum>.Build("foo"), (DummyEnum)value, TimeSpan.Zero);

      if (type == typeof(MetricValue))
        return Property<MetricValue>.Create(PropertyUrn<MetricValue>.Build("foo"),
          new MetricValue((float)value, TimeSpan.Zero, TimeSpan.Zero), TimeSpan.Zero);

      throw new NotImplementedException();
    }

    public struct DummyFloat : IFloat
    {
      private readonly float _value;

      public DummyFloat(float value)
      {
        _value = value;
      }

      public float ToFloat() => _value;
    }

    public enum DummyEnum
    {
      A = 1,
      B = 0,
      C = -1
    }
  }
}