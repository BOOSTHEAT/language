using System;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

[TestFixture]
public class MetricValueTests
{
    [Test]
    public void FromString()
    {
        var result = MetricValue.FromString("1.5");
        Check.That(result.IsSuccess).IsTrue();
        Check.That(result.GetValueOrDefault()).IsEqualTo(new MetricValue(1.5f, TimeSpan.Zero, TimeSpan.Zero));
    }
    
}