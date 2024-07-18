using System;

// ReSharper disable once CheckNamespace
namespace ImpliciX.Language.Model
{
  [ValueObject]
  public readonly struct SubsystemState: IPublicValue
  {
    public Enum State { get; }

    [ModelFactoryMethod]
    public static SubsystemState Create(Enum state) => new (state);

    private SubsystemState(Enum state)
    {
      State = state;
    }

    public override string ToString() => State.ToString();
    public object PublicValue()
    {
      return Convert.ToInt32(State);
    }
  }
}