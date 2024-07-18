using System;
using System.Globalization;
using System.Reflection;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

[TestFixture(typeof(Power))]
[TestFixture(typeof(Energy))]
[TestFixture(typeof(Volume))]
[TestFixture(typeof(Mass))]
[TestFixture(typeof(AngularSpeed))]
[TestFixture(typeof(Current))]
[TestFixture(typeof(DifferentialPressure))]
[TestFixture(typeof(DifferentialTemperature))]
[TestFixture(typeof(Flow))]
[TestFixture(typeof(Percentage))]
[TestFixture(typeof(Pressure))]
[TestFixture(typeof(RotationalSpeed))]
[TestFixture(typeof(StandardLiterPerMinute))]
[TestFixture(typeof(Temperature))]
[TestFixture(typeof(Voltage))]
[TestFixture(typeof(Duration))]
[TestFixture(typeof(Torque))]
[TestFixture(typeof(Length))]
[TestFixture(typeof(MyModel.MyCustomUnit))]
[TestFixture(typeof(MyModel.MyOtherCustomUnit))]
public class FloatValuesTests<T> where T : IFloat<T>
{
    private CultureInfo originalCulture;

    [SetUp]
    public void Setup()
    {
        originalCulture = CultureInfo.CurrentCulture;
    }

    [Test]
    public void CreateFromFloat()
    {
        Assert.That(FromFloat(3.14f).Value.ToFloat(), Is.EqualTo(3.14f));
    }

    [Test]
    public void CreateFromString_from_FR_culture()
    {
        CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
        var value = Invoke("FromString", "3.14")!;
        Assert.That(value, Is.InstanceOf<Result<T>>());
        var typedValue = (Result<T>)value;
        Assert.That(typedValue.Value.ToFloat(), Is.EqualTo(3.14f));
    }

    [Test]
    public void CreateFromString_from_US_culture()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en_US");
        var value = Invoke("FromString", "3.14")!;
        Assert.That(value, Is.InstanceOf<Result<T>>());
        var typedValue = (Result<T>)value;
        Assert.That(typedValue.Value.ToFloat(), Is.EqualTo(3.14f));
    }

    [Test]
    public void TestEquality()
    {
        var value1 = FromFloat(3.14f).Value;
        var value2 = FromFloat(3.14f).Value;
        Assert.True(value1.Equals(value2));
    }
    
    [Test]
    public void TestNaNEquality()
    {
        var value1 = FromFloat(float.NaN).Value;
        var value2 = FromFloat(float.NaN).Value;
        Assert.True(value1.Equals(value2));
    }

    [Test]
    public void TestObjectEquality()
    {
        var value1 = (object)FromFloat(3.14f).Value;
        var value2 = (object)FromFloat(3.14f).Value;
        Assert.True(value1.Equals(value2));
    }

    private static Result<T> FromFloat(float f)
    {
        var value = Invoke("FromFloat", f)!;
        Assert.That(value, Is.InstanceOf<Result<T>>());
        return (Result<T>)value;
    }

    private static object Invoke(string methodName, object arg) =>
        GetMethod(methodName).Invoke(null, new object[] { arg })!;
    
    private static MethodInfo GetMethod(string name) =>
        typeof(T).GetMethod(name, BindingFlags.Public | BindingFlags.Static)!;

    [TearDown]
    public void Cleanup()
    {
        CultureInfo.CurrentCulture = originalCulture;
    }
}