using System.Collections.Generic;
using System.Linq;
namespace ImpliciX.Language.Core
{
    public class Error
    {
        private List<(string key, string message)> _errorContents;

        public Error(string key, string message)
        {
            _errorContents = new List<(string key, string message)> {(key, message)};
        }

        private Error(IEnumerable<(string key, string message)> content)
        {
            _errorContents = new List<(string key, string message)>(content);
        }

        public string Message => string.Join(";", _errorContents.Select(err => $"{err.key}:{err.message}"));

        public IReadOnlyList<(string key, string value)> Content => _errorContents.AsReadOnly();

        public bool IsEmpty()
        {
            return !Content.Any();
        }

        public Error Merge(Error other)
        {
            var newContent = _errorContents.Concat(other.Content).ToList();
            return new Error(newContent);
        }

        public override bool Equals(object obj)
        {
            return  obj is Error error && obj.GetType() == this.GetType() && !error.Content.Except(this.Content).Any();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_errorContents.GetHashCode() * 397) ^ Message.GetHashCode();
            }
        }
    }
}
