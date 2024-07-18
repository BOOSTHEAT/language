using ImpliciX.Language.Model;

namespace ImpliciX.Language.Store
{
    public class FirewallRule
    {
        public static Builder RejectAll() =>
            new Builder(DecisionKind.Reject);
        
        public static Builder Reject<T>(CommandNode<T> cmd) =>
            new Builder(DecisionKind.Reject, cmd.Urn);

        public class Builder
        {
            public FirewallRule From(string moduleId)
                => new FirewallRule(moduleId, DirectionKind.From, _decision, _urns);

            internal Builder(DecisionKind decision, params Urn[] urns)
            {
                _decision = decision;
                _urns = urns;
            }

            private DecisionKind _decision;
            private Urn[] _urns;
        }
        public string ModuleId { get; }
        public DirectionKind Direction { get; }
        public DecisionKind Decision { get; }
        public Urn[] Urns { get; }

        public enum DirectionKind
        {
            From
        }

        public enum DecisionKind
        {
            Reject
        }
        
        private FirewallRule(string moduleId, DirectionKind direction, DecisionKind decision, Urn[] urns)
        {
            ModuleId = moduleId;
            Direction = direction;
            Decision = decision;
            Urns = urns;
        }
    }
    

}