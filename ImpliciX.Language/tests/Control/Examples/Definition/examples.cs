using ImpliciX.Language.Model;
#pragma warning disable CS8981

namespace ImpliciX.Language.Tests.Control.Examples.Definition
{
    public class examples : RootModelNode
    {
        public examples() : base(nameof(examples))
        {
        }
        public static always always => new always(new examples());
        public static optimizationSubsystem optimizationSubsystem => new optimizationSubsystem(new examples());
    }

    public class optimizationSubsystem : SubSystemNode
    {
        public optimizationSubsystem(ModelNode parent) : base(nameof(optimizationSubsystem), parent)
        {
        }

        public PropertyUrn<Literal> xprop => PropertyUrn<Literal>.Build(Urn, nameof(xprop));
        public PropertyUrn<Temperature> propA => PropertyUrn<Temperature>.Build(Urn, nameof(propA));
        public PropertyUrn<Temperature> propB => PropertyUrn<Temperature>.Build(Urn, nameof(propB));
        public PropertyUrn<Temperature> propC => PropertyUrn<Temperature>.Build(Urn, nameof(propC));
        public PropertyUrn<Temperature> propD => PropertyUrn<Temperature>.Build(Urn, nameof(propD));
        public PropertyUrn<Temperature> prop25 => PropertyUrn<Temperature>.Build(Urn, nameof(prop25));
        public PropertyUrn<Temperature> prop50 => PropertyUrn<Temperature>.Build(Urn, nameof(prop50));
        public PropertyUrn<Temperature> prop100 => PropertyUrn<Temperature>.Build(Urn, nameof(prop100));
        public PropertyUrn<Duration> timer => PropertyUrn<Duration>.Build(Urn, nameof(timer));
        public CommandUrn<NoArg> _toB => CommandUrn<NoArg>.Build(Urn, "TOB");
        public CommandUrn<Temperature> _commandA => CommandUrn<Temperature>.Build(Urn, "COMMANDA");
        public CommandUrn<NoArg> _toAb => CommandUrn<NoArg>.Build(Urn, "TOAB");
        public CommandUrn<NoArg> _toAa => CommandUrn<NoArg>.Build(Urn, "TOAA");
        public PropertyUrn<Literal> public_state => PropertyUrn<Literal>.Build(Urn, nameof(public_state));

        public PropertyUrn<OptimizationSubsystem.PublicState> optimization_subsystem_public_state =>
            PropertyUrn<OptimizationSubsystem.PublicState>.Build(Urn, nameof(optimization_subsystem_public_state));
    }

    public class always : SubSystemNode
    {
        public always(ModelNode parent) : base(nameof(always), parent)
        {
        }

        public PropertyUrn<Literal> xprop => PropertyUrn<Literal>.Build(Urn, nameof(xprop));
        public PropertyUrn<Literal> yprop => PropertyUrn<Literal>.Build(Urn, nameof(yprop));
        public PropertyUrn<Literal> yprop_default => PropertyUrn<Literal>.Build(Urn, nameof(yprop_default));
        public PropertyUrn<Temperature> propA => PropertyUrn<Temperature>.Build(Urn, nameof(propA));
        public PropertyUrn<Temperature> propC => PropertyUrn<Temperature>.Build(Urn, nameof(propC));
        public PropertyUrn<Temperature> prop25 => PropertyUrn<Temperature>.Build(Urn, nameof(prop25));
        public PropertyUrn<Temperature> prop100 => PropertyUrn<Temperature>.Build(Urn, nameof(prop100));
        public CommandUrn<NoArg> _toB => CommandUrn<NoArg>.Build(Urn, "TOB");
        public CommandUrn<NoArg> _toAb => CommandUrn<NoArg>.Build(Urn, "TOAB");
        public CommandUrn<NoArg> _toAa => CommandUrn<NoArg>.Build(Urn, "TOAA");
        public PropertyUrn<AlwaysSubsystem.PublicState> always_public_state => PropertyUrn<AlwaysSubsystem.PublicState>.Build(Urn, nameof(always_public_state));
        public PropertyUrn<Percentage> zprop => PropertyUrn<Percentage>.Build(Urn, nameof(zprop));
        public PropertyUrn<Percentage> tprop => PropertyUrn<Percentage>.Build(Urn, nameof(tprop));
        public PropertyUrn<FunctionDefinition> func => PropertyUrn<FunctionDefinition>.Build(Urn, nameof(func));
    }
}