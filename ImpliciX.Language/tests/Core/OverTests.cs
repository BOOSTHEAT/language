using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Core;

public class OverTests
{
  [TestCase(1, TimeUnit.Years, "366")]
  [TestCase(1, TimeUnit.Quarters, "92")]
  [TestCase(1, TimeUnit.Months, "31")]
  [TestCase(1, TimeUnit.Weeks, "7")]
  [TestCase(1, TimeUnit.Days, "1")]
  [TestCase(1, TimeUnit.Hours, "1:0")]
  [TestCase(1, TimeUnit.Minutes, "0:1")]
  [TestCase(1, TimeUnit.Seconds, "0:0:1")]
  [TestCase(1, TimeUnit.Milliseconds, "0:0:0.001")]
  [TestCase(10, TimeUnit.Years, "3660")]
  [TestCase(11, TimeUnit.Quarters, "1012")]
  [TestCase(15, TimeUnit.Months, "465")]
  [TestCase(13, TimeUnit.Weeks, "91")]
  [TestCase(14, TimeUnit.Days, "14")]
  [TestCase(15, TimeUnit.Hours, "15:0")]
  [TestCase(16, TimeUnit.Minutes, "0:16")]
  [TestCase(17, TimeUnit.Seconds, "0:0:17")]
  [TestCase(18, TimeUnit.Milliseconds, "0:0:0.018")]
  public void Nominal(int multiplier, TimeUnit timeUnit, string timeSpan)
  {
    var past = Over.ThePast(multiplier);
    var result = timeUnit switch
    {
      TimeUnit.Years => past.Years,
      TimeUnit.Quarters => past.Quarters,
      TimeUnit.Months => past.Months,
      TimeUnit.Weeks => past.Weeks,
      TimeUnit.Days => past.Days,
      TimeUnit.Hours => past.Hours,
      TimeUnit.Minutes => past.Minutes,
      TimeUnit.Seconds => past.Seconds,
      TimeUnit.Milliseconds => past.Milliseconds,
      _ => throw new ArgumentOutOfRangeException(nameof(timeUnit), timeUnit, null)
    };
    Assert.That(result.Multiplier, Is.EqualTo(multiplier));
    Assert.That(result.Unit, Is.EqualTo(timeUnit));
    Assert.That(result.TimeSpan, Is.EqualTo(TimeSpan.Parse(timeSpan)));
  }

  Over<Foo> Over => new ((m,u,f) => new Foo(m,u, f(m)));

  class Foo
  {
    public int Multiplier { get; }
    public TimeUnit Unit { get; }
    public TimeSpan TimeSpan { get; }

    public Foo(int multiplier, TimeUnit unit, TimeSpan timeSpan)
    {
      Multiplier = multiplier;
      Unit = unit;
      TimeSpan = timeSpan;
    }
  }
}