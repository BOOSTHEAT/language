using System.Linq;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using ImpliciX.Language.Tests.Control.Examples;
using ImpliciX.Language.Tests.Control.Examples.Definition;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Control
{
    [TestFixture]
    public class GetOnlyConcernedUrnsTests
    {
        private optimizationSubsystem self = examples.optimizationSubsystem;

        [Test]
        public void get_only_concerned_urns_for_always()
        {
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            var urns = DefinitionProcessing.GetUrnsConcernedByAlways(optimizationSubsystemDefinition.Always);

            Urn[] expected =
            {
                self.Urn,
                self.propA,
                self.prop25,
                self.prop100
            };

            Check.That(urns).Equals(expected);
        }

        [Test]
        public void get_only_concerned_urns_for_state()
        {
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            var urns = DefinitionProcessing.GetUrnsConcernedByStateForPropertiesChanged(
                optimizationSubsystemDefinition._stateDefinitions[OptimizationSubsystem.PrivateState.A]);

            Urn[] expected =
            {
                self.Urn,
                self.propA,
                self.prop25,
                self.prop100,
            };

            Check.That(urns).Equals(expected);
        }

        [TestCase(OptimizationSubsystem.PrivateState.Aa, true)]
        [TestCase(OptimizationSubsystem.PrivateState.Ab, false)]
        public void get_only_concerned_state_for_systemTicked(OptimizationSubsystem.PrivateState state, bool expected)
        {
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            DefinitionProcessing.AddMetaDataToDefinition(optimizationSubsystemDefinition);
            var isConcerned = DefinitionProcessing.IsConcernedBySystemTicked(optimizationSubsystemDefinition, state);
            Check.That(isConcerned).IsEqualTo(expected);
        }

        [Test]
        public void get_only_concerned_command_for_commandRequested()
        { 
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            DefinitionProcessing.AddMetaDataToDefinition(optimizationSubsystemDefinition);
            var urns = DefinitionProcessing.GetCurrentConcernedCommandRequestedUrns(optimizationSubsystemDefinition, OptimizationSubsystem.PrivateState.Aa);
            Urn[] expected =
            {
                self._toAb,
                self._toB,
            };
            
            Check.That(urns.OrderBy(c => c.Value)).ContainsExactly(expected.OrderBy(c => c.Value));
        }
        
        [Test]
        public void get_only_concerned_command_for_TimeoutOccured()
        { 
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            DefinitionProcessing.AddMetaDataToDefinition(optimizationSubsystemDefinition);
            var urns = DefinitionProcessing.GetCurrentConcernedTimeoutUrns(optimizationSubsystemDefinition, OptimizationSubsystem.PrivateState.Aa);
            Urn[] expected =
            {
                self.timer
            };
            
            Check.That(urns.OrderBy(c => c.Value)).ContainsExactly(expected.OrderBy(c => c.Value));
        }

        [Test]
        public void get_only_concerned_urns_for_state_composite()
        {
            var optimizationSubsystemDefinition = new OptimizationSubsystem();
            DefinitionProcessing.AddMetaDataToDefinition(optimizationSubsystemDefinition);
            var urns = DefinitionProcessing.GetCurrentConcernedUrnsForPropertiesChanged(optimizationSubsystemDefinition,
                OptimizationSubsystem.PrivateState.Aa);

            Urn[] expected =
            {
                self.prop25,
                self.prop100,
                self.state,
                self.Urn,
                self.propA,
            };
            Check.That(urns.OrderBy(c => c.Value)).ContainsExactly(expected.OrderBy(c => c.Value));
        }
    }
}