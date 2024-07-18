
namespace ImpliciX.Language.Model
{
    [ValueObject]
    public enum AlarmReset
    {
        Auto = -1,  // Automatic alarm
        Yes = 1,   // manual alarm, ready to reset
        No = 0     // manual alarm, not ready
    }
}
