using ImpliciX.Language.Model;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model;

public class UrnTests
{
    [TestCaseSource(nameof(_isPartOfCases))]
    public void is_part_of(Urn child, Urn parent,  bool expected)
    {
        var actual = child.IsPartOf(parent);
        Assert.AreEqual(expected, actual);
    }

    private static object[] _isPartOfCases =
    {
        new object[] {(Urn)"foo", (Urn)"foo", false},
        new object[] {(Urn)"foo", (Urn)"foo:bar", true},
        new object[] {(Urn)"foo:bar", (Urn)"foo", false},
        new object[] {(Urn)"foo:bar", (Urn)"foo:bar", false},
        new object[] {(Urn)"foo:bar", (Urn)"foo:bar:baz", true},
        new object[] {(Urn)"foo:bar:baz", (Urn)"foo:baz", false},
    };
    
    [TestCaseSource(nameof(_tryRemoveRootCases))]
    public void try_remove_root(Urn root, Urn urn, bool expectedResult, Urn expected)
    {
        var actualResult = urn.TryRemoveRoot(root, out var actual);
        Assert.AreEqual(expectedResult, actualResult);
        Assert.AreEqual(expected, actual);
    }
    
    [TestCaseSource(nameof(_removeRootCases))]
    public void remove_root(Urn root, Urn urn, Urn expected)
    {
        var actual = urn.RemoveRoot(root);
        Assert.AreEqual(expected, actual);
    }

    private static object[] _tryRemoveRootCases =
    {
        new object[] {(Urn) "foo", (Urn) "foo", false, null},
        new object[] {(Urn) "foo", (Urn) "foo:foo", true, (Urn) "foo"},
        new object[] {(Urn) "foo", (Urn) "foo:bar", true, (Urn) "bar"},
        new object[] {(Urn) "foo:bar", (Urn) "foo:bar:baz:fizz", true, (Urn) "baz:fizz"},
        new object[] {(Urn) "bar", (Urn) "foo:bar:baz", false, null},
    };
    
    private static object[] _removeRootCases =
    {
        new object[] {(Urn) "foo", (Urn) "foo:foo", (Urn) "foo"},
        new object[] {(Urn) "foo", (Urn) "foo:bar", (Urn) "bar"},
        new object[] {(Urn) "foo:bar", (Urn) "foo:bar:baz:fizz", (Urn) "baz:fizz"},
    };

}