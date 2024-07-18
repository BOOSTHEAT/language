using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Modbus;

public class CommandMap
{
  public static ICommandMap Empty() => CreateCommandMap();

  private static Func<ICommandMap> CreateCommandMap =>
    Factory ?? throw new Exception("CommandMap factory was not defined");

  public static Func<ICommandMap> Factory { private get; set; }
}

public interface ICommandMap
{
  ICommandMap Add<T>(CommandNode<T> commandNode, CommandActuator actuatorFunc);
  CommandActuator ModbusCommandFactory(Urn urn);
  bool ContainsKey(Urn commandUrn);
  IMeasure Measure(Urn commandUrn);
}

public delegate Result<Command> CommandActuator(object arg, TimeSpan ts, IDriverState state);