using System.Collections.Generic;
using ImpliciX.Language.Model;
namespace ImpliciX.Language.Control
{
    public class OnState
    {
        public readonly List<SetWithComputation> _setWithComputations = new List<SetWithComputation>();
        public readonly List<SetPeriodical> _setPeriodical = new List<SetPeriodical>();
        public readonly List<SetWithProperty> _setWithProperties = new List<SetWithProperty>();
        public readonly Dictionary<Urn, SetWithConditions> _setWithConditions =
            new Dictionary<Urn, SetWithConditions>();
    }
}