using ImpliciX.Language.Model;

namespace ImpliciX.Language.Control
{
    public class SetPeriodical : SetWithComputation
    {
        public SetPeriodical(Urn urnToSet, FuncRef funcRef, PropertyUrn<FunctionDefinition> funcDefUrn, params Urn[] urns) : base(urnToSet, funcRef, funcDefUrn, urns)
        {
        }
    }
}