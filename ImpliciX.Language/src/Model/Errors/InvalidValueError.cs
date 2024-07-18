using ImpliciX.Language.Core;
namespace ImpliciX.Language.Model
{
    public class InvalidValueError : Error
    {
        public InvalidValueError(string message) : base(nameof(InvalidValueError), message)
        {
        }
        public static InvalidValueError Create(string message) => new InvalidValueError(message);
    }
}