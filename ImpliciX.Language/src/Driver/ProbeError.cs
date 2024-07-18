using ImpliciX.Language.Core;
namespace ImpliciX.Language.Driver
{
    public class ProbeError : Error
    {
        public ProbeError() : base("PROBE_ERROR", "Input out of normal range")
        {
        }
    }
}