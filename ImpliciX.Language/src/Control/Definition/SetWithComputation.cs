using System.Linq;
using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class SetWithComputation
    {
        public readonly Urn _urnToSet;
        public readonly FuncRef _funcRef;
        public readonly PropertyUrn<FunctionDefinition> _funcDefUrn;
        public readonly Urn[] _xUrns;
        public Urn[] TriggersUrn { get; }

        public SetWithComputation(Urn urnToSet, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDefUrn, params Urn[] urns)
        {
            _urnToSet = urnToSet;
            _funcRef = funcRef;
            _funcDefUrn = funcDefUrn;
            _xUrns = urns;
            TriggersUrn = funcRef.TriggerSelector(urns).Append(_funcDefUrn).ToArray();
        }
    }
}