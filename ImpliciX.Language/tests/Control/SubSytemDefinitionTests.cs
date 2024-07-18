using ImpliciX.Language.Control;
using ImpliciX.Language.Tests.Control.Examples;
using ImpliciX.Language.Tests.Control.Examples.Definition;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Control;

public class SubSytemDefinitionTests
{
    [Test]
    public void subsystem_definition_should_expose_its_state_urn()
    {
        ISubSystemDefinition sut = new AutomaticStore();
        Check.That(sut.StateUrn).IsEqualTo(domotic.automatic_store.state);
    }
    
    [Test]
    public void subsystem_definition_should_expose_its_state_type()
    {
        ISubSystemDefinition sut = new AutomaticStore();
        Check.That(sut.StateType).IsEqualTo(typeof(AutomaticStore.State));
    }
}