using System;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

public class PublicValuesTests
{
    private static TestCaseData[] propertiesTestCaseDatas = {
        new(AngularSpeed.FromFloat(25f).Value, 25f),
        new(Counter.FromInteger(25uL), 25uL),
        new(Current.FromFloat(25f).Value, 25f),
        new(DifferentialPressure.FromFloat(25f).Value, 25f),
        new(DifferentialTemperature.FromFloat(25f).Value, 25f),
        new(DisplacementQueue.FromShort(25).Value, 25),
        new(Duration.FromFloat(25).Value, 25),
        new(Energy.FromFloat(25).Value, 25),
        new(ExpansionValveAbsolutePosition.FromString("25").Value, 25),
        new(Flow.FromFloat(25).Value, 25),
        new(FunctionDefinition.From(new []{("a",1f), ("b", 2f)}).Value, "a:1|b:2"),
        new(Literal.Create("toto"), "toto"),
        new(Text10.Create("foobarfizz"), "foobarfizz"),
        new(Text50.Create("yet_another_text_value"),"yet_another_text_value"),
        new(Text200.Create("this_can_be_a_longer_text_value"),"this_can_be_a_longer_text_value"),
        new(Percentage.FromFloat(0.5f).Value, 0.5f),
        new(Power.FromFloat(25).Value, 25),
        new(RotationalSpeed.FromFloat(25).Value, 25),
        new(Pressure.FromFloat(25).Value, 25),
        new(StandardLiterPerMinute.FromFloat(25).Value, 25),
        new(SubsystemState.Create(Presence.Enabled),Convert.ToInt32(Presence.Enabled)),
        new(Temperature.FromFloat(32f).Value, 32f),
        new(Voltage.FromFloat(32f).Value, 32f),
        new(Volume.FromFloat(32f).Value, 32f),
        new(MetricValue.FromString("1.5").Value, 1.5f),
        new(Duration.FromString("1.5").Value, 1.5f),
        new(Torque.FromString("1.5").Value, 1.5f),
        new(Length.FromString("1.5").Value, 1.5f),
    };

    [TestCaseSource(nameof(propertiesTestCaseDatas))]
    public void test_public_values(IPublicValue sut, object expectedValue)
    {
        Check.That(sut.PublicValue()).IsEqualTo(expectedValue);
    }
}