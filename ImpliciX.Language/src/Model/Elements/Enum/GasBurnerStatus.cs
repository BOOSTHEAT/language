
namespace ImpliciX.Language.Model
{
    [ValueObject]
    public enum GasBurnerStatus
    {
        NotSupplied = 0,
        Ready = 1,
        Ignited = 2,
        Faulted = -1
    }
}