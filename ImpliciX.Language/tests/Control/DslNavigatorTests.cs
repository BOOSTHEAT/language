using System.Linq;
using ImpliciX.Language.Control;
using ImpliciX.Language.Tests.Control.Examples;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Control
{
    [TestFixture]
  public class DslNavigatorTests
  {
    public AutomaticStore definition;
    public DslNavigator<AutomaticStore.State, string, string> sut;

    [Test]
    public void should_get_state_definitions()
    {
      var stateDefinitions = sut.GetStates(definition);
      Check.That(stateDefinitions.Keys).ContainsExactly("FullyOpen", "ClosureInProgress", "FullyClosed");
      Check.That(stateDefinitions.Values).ContainsNoNull();
    }

    [Test]
    public void should_get_state_operations_of_single_kind()
    {
      var sut = new DslNavigator<AutomaticStore.State, string, string>();
      sut.DefineSection(s => s.OnEntry)
        .DescribeOperation(p => p._setsValues, x => $"{x._urn.Value} = {x._value}");
      var setValuesOnFullyClosedEntry = sut.GetOperationsIn(sut.GetStates(definition)["FullyClosed"]);
      Check.That(setValuesOnFullyClosedEntry).ContainsExactly(
        "domotic:secondary_store:CLOSEWITHPARAM = Full",
        "domotic:automatic_store:light:SWITCH = On"
        );
    }
    
    [Test]
    public void should_get_state_operations_of_multiple_kinds()
    {
      sut.DefineSection(s => s.OnEntry)
        .DescribeOperation(p => p._setsValues, x => $"OnEntryValue: {x._urn.Value} = {x._value}")
        .DescribeOperation(p => p._setsWithProperties,
          x => $"OnEntryProperties: {x._urn.Value} = {x._propertyUrn.Value}");
      sut.DefineSection(s => s.OnState)
        .DescribeOperation(p => p._setWithProperties, x => $"OnStateProperties: {x._urn.Value} = {x._propertyUrn.Value}")
        .DescribeOperation(p => p._setWithComputations, x =>
          $"OnStateComputations: {x._urnToSet} = {x._funcRef.Name}({x._funcDefUrn.Value},{string.Join(',', x._xUrns.Select(y => y.Value))})");
      
      var allOperationsOfClosureInProgress = sut.GetOperationsIn(sut.GetStates(definition)["FullyClosed"]);
      Check.That(allOperationsOfClosureInProgress)
        .ContainsExactly("OnEntryValue: domotic:secondary_store:CLOSEWITHPARAM = Full",
          "OnEntryValue: domotic:automatic_store:light:SWITCH = On",
          "OnEntryProperties: domotic:automatic_store:light:Change_Color = domotic:automatic_store:light_settings:White",
          "OnEntryProperties: domotic:automatic_store:light_settings:default_intensity = constant:parameters:percentage:one",
          "OnStateProperties: domotic:automatic_store:light:INTENSITY = domotic:automatic_store:light_settings:INTENSITY",
          "OnStateProperties: domotic:automatic_store:light_settings:INTENSITY = domotic:automatic_store:light_settings:default_intensity",
          "OnStateProperties: domotic:automatic_store:light:Change_Color = domotic:automatic_store:light_settings:White"
          );
    }
    
    [Test]
    public void should_get_state_transitions_of_single_kind()
    {
      sut.DefineSection(s => s.Transitions)
        .DescribeTransition(t=> t._whenMessages, t=>t._state, x=>($"{x._urn}({x._value})"));
      
      var messageTransitionsFromFullyClosed = sut.GetTransitionsFrom(sut.GetStates(definition)["FullyClosed"], sut.GetStates(definition)).First().Value;
      Check.That(messageTransitionsFromFullyClosed).ContainsExactly("domotic:automatic_store:OPEN(Full)");
    }
    
    [Test]
    public void should_get_state_transitions_of_multiple_kinds()
    {
      sut.DefineSection(s => s.Transitions)
        .DescribeTransition(t=> t._whenMessages, t=>t._state, x=>($"{x._urn}({x._value})"))
        .DescribeTransition(t=> t._whenTimeouts, t=>t._state, x => ($"Timeout {x._timerUrn}"));
      
      var allTransitionsFromClosureInProgress = sut.GetTransitionsFrom(sut.GetStates(definition)["ClosureInProgress"], sut.GetStates(definition)).First().Value;
      Check.That(allTransitionsFromClosureInProgress).ContainsExactly(
        "domotic:automatic_store:CLOSED()",
        "Timeout domotic:automatic_store:TIMER");
    }


    [Test]
    public void should_get_operations_always()
    {
      var sut = new DslNavigator<AlwaysSubsystem.PrivateState, string, string>();
      var definitionSubsystem = new AlwaysSubsystem();
      sut.DefineSection(s => s.Always)
        .DescribeOperation(p => p._setWithComputations, x =>
          $"{x._urnToSet} = {x._funcRef.Name}({x._funcDefUrn.Value},{string.Join(',', x._xUrns.Select(y => y.Value))})")
        .DescribeOperation(p => p._setWithConditions.Values, set =>
          $"{set._setUrn} => {set._withs.Aggregate("", (acc, with) => { if (with._isValueUrn) return $"{acc} {with._valueUrn}"; else return $"{acc} {with._value}"; })}");      sut.DefineSection(s => s.OnExit);
      
      var globalOperations = sut.GetGlobalOperations(definitionSubsystem);
      Check.That(globalOperations).ContainsExactly(
        "examples:always:zprop = Polynomial1(examples:always:func,examples:always:tprop)",
        "examples:always:always_public_state =>  PublicA Other",
        "examples:always:xprop =>  Value1 Value2 Value3 Value4",
        "examples:always:yprop =>  examples:always:yprop_default Otherwise");
    }
    
    // toto:titi => value1, value2, value3
    
    [SetUp]
    public void Init()
    {
      definition = new AutomaticStore();
      sut = new DslNavigator<AutomaticStore.State, string, string>();
    }
  }

}