using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using Moq;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Modbus;

public class CommandMapTests
{
  [SetUp]
  public void EachTestMustRunAsIfTheApplicationWasJustStarted()
  {
    CommandMap.Factory = null;
  }
  
  [Test]
  public void DeclareMap()
  {
    var commandMap = new Mock<ICommandMap>();
    commandMap.Setup(cm => cm.Add(_commandNode1, It.IsAny<CommandActuator>()))
      .Returns(commandMap.Object);
    commandMap.Setup(cm => cm.Add(_commandNode2, It.IsAny<CommandActuator>()))
      .Returns(commandMap.Object);
    
    CommandMap.Factory = () => commandMap.Object;
    
    var map = CommandMap.Empty()
      .Add(_commandNode1, GetActuator<Pressure>(10))
      .Add(_commandNode2, GetActuator<Temperature>(20));
    
    Assert.That(map, Is.EqualTo(commandMap.Object));
  }

  private readonly CommandNode<Pressure> _commandNode1 = CommandNode<Pressure>.Create("pressure", new RootModelNode("root"));
  private readonly CommandNode<Temperature> _commandNode2 = CommandNode<Temperature>.Create("temperature", new RootModelNode("root"));

  private CommandActuator GetActuator<T>(ushort commandId) where T:IFloat =>
  (arg, ts, state) =>
    from value in arg.RCast<T>()
    from encoded in new Func<ushort>(() => Convert.ToUInt16(value.ToFloat()))
      .RInvoke(exception => new InvalidValueError($"{value} is invalid"))
    let cmd = Command.Create(commandId, new[] {encoded})
    select cmd;
}