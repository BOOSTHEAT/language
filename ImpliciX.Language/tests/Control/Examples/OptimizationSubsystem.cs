using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using ImpliciX.Language.Tests.Control.Examples.Definition;
using ImpliciX.Language.Tests.Control.Examples.Functions;
using static ImpliciX.Language.Control.Condition;

namespace ImpliciX.Language.Tests.Control.Examples
{
    public class OptimizationSubsystem : SubSystemDefinition<OptimizationSubsystem.PrivateState>
    {
        public enum PrivateState
        {
            A,
            Aa,
            Ab,
            B
        }

        public enum PublicState
        {
            PublicA,
            Other
        }

        public OptimizationSubsystem()
        {
            // @formatter:off
            var self = examples.optimizationSubsystem;
            Subsystem(self)
                .Always
                    .Set(self.optimization_subsystem_public_state)
                        .With(PublicState.PublicA).When(InState(self, PrivateState.A))
                        .With(PublicState.Other).Otherwise()
                    .Set(self.xprop)
                        .With(Literal.Create("Value1")).When(LowerThan(self.propA, self.prop25))
                        .With(Literal.Create("Value2")).When(LowerThan(self.propA, self.prop25))
                        .With(Literal.Create("Value3")).When(LowerThan(self.propA, self.prop100))
                        .With(Literal.Create("Value4")).Otherwise()
                .Initial(PrivateState.A)
                    .Define(PrivateState.A)
                        .Transitions
                            .WhenMessage(self._toB, default).Then(PrivateState.B)
                            .WhenTimeout(self.timer).Then(PrivateState.B)
                    .Define(PrivateState.Aa).AsInitialSubStateOf(PrivateState.A)
                        .OnEntry
                            .Set(self.propA,self.prop25)
                        .OnState
                            .Set(self.propA,self.prop25)
                            .Set(self.propB,self.prop25)
                            .Set(self.propC,self.prop100)
                            .SetPeriodical(self.propD,Identity.Func,constant.parameters.none,self.prop100)
                        .OnExit
                            .Set(self.propA,Temperature.Create(50))
                            .Set(self._commandA,self.prop50)
                        .Transitions
                            .WhenMessage(self._toAb, default).Then(PrivateState.Ab)
                    .Define(PrivateState.Ab).AsSubStateOf(PrivateState.A)
                        .Transitions
                            .WhenMessage(self._toAa, default).Then(PrivateState.Aa)
                    .Define(PrivateState.B);
                // @formatter:on
        }
    }
}