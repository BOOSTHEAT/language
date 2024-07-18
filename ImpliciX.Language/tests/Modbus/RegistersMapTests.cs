using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using Moq;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Modbus;

public class RegistersMapTests
{
  [SetUp]
  public void EachTestMustRunAsIfTheApplicationWasJustStarted()
  {
    RegistersMap.Factory = null;
  }
  
  [Test]
  public void DeclareMap()
  {
    var registersMap = new Mock<IRegistersMap>();
    var slice1 = new Mock<IConversionDefinition>();
    registersMap.Setup(rm => rm.For(_measureNode1)).Returns(slice1.Object);
    slice1
      .Setup(s => s.DecodeRegisters(10, 20, It.IsAny<MeasureDecoder>()))
      .Returns(registersMap.Object);
    var slice2 = new Mock<IConversionDefinition>();
    registersMap.Setup(rm => rm.For(_measureNode2)).Returns(slice2.Object);
    slice2
      .Setup(s => s.DecodeRegisters(30, 40, It.IsAny<MeasureDecoder>()))
      .Returns(registersMap.Object);
    
    RegistersMap.Factory = () => registersMap.Object;
    
    var map = RegistersMap.Create()
      .For(_measureNode1)
      .DecodeRegisters(10, 20, GetDecoder(data => Pressure.FromFloat(42).Value ))
      .For(_measureNode2)
      .DecodeRegisters(30, 40, GetDecoder(data => Temperature.FromFloat(43).Value ));
    
    Assert.That(map, Is.EqualTo(registersMap.Object));
  }

  private readonly MeasureNode<Pressure> _measureNode1 = new ("pressure", new RootModelNode("root"));
  private readonly MeasureNode<Temperature> _measureNode2 = new ("temperature", new RootModelNode("root"));

  private MeasureDecoder GetDecoder<T>(Func<ushort[], T> convert) =>
  (measureUrn, measureStatus, measureRegisters,
    currentTime, driverStateKeeper) => Result<IMeasure>
    .Create(Measure<T>.Create(measureUrn, measureStatus, Result<T>.Create(convert(measureRegisters)), currentTime));
}