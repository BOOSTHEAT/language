
namespace ImpliciX.Language.Model
{
    [ValueObject]
    public enum MotorCurrent
    {
        Normal = 0,
        SoftwareDisjunction = 1, 
        HardwareDisjunction = 2,
        DeRating = 3
    }
}