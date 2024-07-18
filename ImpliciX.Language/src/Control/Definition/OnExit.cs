using System.Collections.Generic;

namespace ImpliciX.Language.Control
{
    public class OnExit
    {
        public readonly List<SetValue> _setsValues = new List<SetValue>();
        public readonly List<SetWithProperty> _setsWithProperty = new List<SetWithProperty>();
    }
}