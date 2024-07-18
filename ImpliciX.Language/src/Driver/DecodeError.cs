using ImpliciX.Language.Core;
namespace ImpliciX.Language.Driver
{
    public class DecodeError : Error
    {
        public DecodeError(string message) : base(nameof(DecodeError), message)
        {
        }
    }
}