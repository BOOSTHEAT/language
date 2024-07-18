using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class OnEntry
    {
        public readonly List<SetValue> _setsValues = new List<SetValue>();
        public readonly List<SetWithComputation> _setsWithComputations = new List<SetWithComputation>();
        public readonly List<SetWithProperty> _setsWithProperties = new List<SetWithProperty>();
        public readonly List<PropertyUrn<Duration>> _startTimers = new List<PropertyUrn<Duration>>();
    }
}