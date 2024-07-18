using ImpliciX.Language.Model;
namespace ImpliciX.Language.Modbus
{
    [ValueObject]
    public enum BoardState
    {
        WaitingForStart,
        UpdateRunning,
        RegulationStarted,
    }
}