using System;
using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

public class DataModelValueTests
{
    [Test]
    public void change_the_urn()
    {
        var mv = new DataModelValue<Temperature>("foo:bar", Temperature.Create(10f), TimeSpan.Zero);
        var mv2 = mv.WithUrn("foo:baz");
        Assert.AreEqual("foo:baz", mv2.Urn.Value);
        Assert.AreEqual(mv.Value, mv2.ModelValue());
        Assert.AreEqual(mv.At, mv2.At);
        Assert.AreNotSame(mv2,mv);
    }
    
    [Test]
    public void change_the_time()
    {
        var mv = new DataModelValue<Temperature>("foo:bar", Temperature.Create(10f), TimeSpan.Zero);
        var mv2 = mv.WithAt(TimeSpan.FromSeconds(10));
        Assert.AreEqual(mv.Urn.Value, mv2.Urn.Value);
        Assert.AreEqual(mv.Value, mv2.ModelValue());
        Assert.AreEqual(TimeSpan.Zero, mv.At);
        Assert.AreEqual(TimeSpan.FromSeconds(10), mv2.At);
        Assert.AreNotSame(mv2,mv);
    }
    
    [Test]
    public void change_the_urn_and_time()
    {
        var mv = new DataModelValue<Temperature>("foo:bar", Temperature.Create(10f), TimeSpan.Zero);
        var mv2 = mv.With("fizz:buzz",TimeSpan.FromSeconds(10));
        Assert.AreEqual("foo:bar", mv.Urn.Value);
        Assert.AreEqual("fizz:buzz", mv2.Urn.Value);
        Assert.AreEqual(mv.Value, mv2.ModelValue());
        Assert.AreEqual(TimeSpan.Zero, mv.At);
        Assert.AreEqual(TimeSpan.FromSeconds(10), mv2.At);
        Assert.AreNotSame(mv2,mv);
    }
}