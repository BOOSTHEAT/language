using System;
using ImpliciX.Language.Core;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Core;

[TestFixture]
public class ResultExtensionsTests
{
    [Test]
    public void should_map_success_result()
    {
        var x = SuccessResultOf("a")
            .Map(s => s.ToUpper());
        Assert.AreEqual("A", x.GetValueOrDefault());
    }

    [Test]
    public void should_map_error_result()
    {
        var x = ErrorResult("err1", "error")
            .Map(s => s.ToUpper());
        Assert.AreEqual(null, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void should_bind_error_result()
    {
        var x = ErrorResult("err1", "error")
            .Bind(s => SafeTryParse(s));
        Assert.AreEqual(0, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void should_bind_success_result()
    {
        var x = SuccessResultOf("1")
            .Bind(s => SafeTryParse(s));
        Assert.AreEqual(1, x.GetValueOrDefault());
        Assert.IsNull(x.Error);
    }

    [Test]
    public void should_unwrap_result_of_result_error()
    {
        var result = Result<Result<string>>.Create(ErrorResult("err1", "error1"));
        var x = result.UnWrap();
        Assert.IsTrue(x.IsError);
        Assert.AreEqual(null, x.GetValueOrDefault());
    }

    [Test]
    public void should_unwrap_result_of_result_success()
    {
        var result = Result<Result<string>>.Create(SuccessResultOf("a"));
        var x = result.UnWrap();
        Assert.IsFalse(x.IsError);
        Assert.AreEqual("a", x.GetValueOrDefault());
    }

    [Test]
    public void when_cast_succeeds_returns_a_success_result()
    {
        object x = "foo";
        var result = x.RCast<string>();
        Check.That(result.IsSuccess).IsTrue();
        Check.That(result.GetValueOrDefault()).IsEqualTo("foo");
    }

    [Test]
    public void when_cast_fails_returns_an_error_result()
    {
        object x = 23;
        var result = x.RCast<string>();
        Check.That(result.IsError).IsTrue();
        Check.That(result.Error.Message).IsEqualTo("Invalid cast:Int32 cannot be cast into String");
    }

    [Test]
    public void when_no_exception_is_raised_returns_a_success_result()
    {
        var result = new Func<string>(() => "1" + "1").RInvoke(_ => null);
        Check.That(result.IsSuccess).IsTrue();
        Check.That(result.GetValueOrDefault()).IsEqualTo("11");
    }

    [Test]
    public void when_an_exception_is_raised_returns_an_error_result_and_do_not_rethrows_the_exception()
    {
        var result = new Func<int>(() => throw new Exception("boom")).RInvoke(e => new SomeError(e));
        Check.That(result.IsError).IsTrue();
        Check.That(result.Error).IsInstanceOf<SomeError>();
        Check.That(result.Error.Message).IsEqualTo("SomeError:boom");
    }

    [Test]
    public void when_error_throw_exception()
    {
        var r1 = Result<int>.Create(new SomeError("error"));
        Check.ThatCode(() => r1.ThrowOnError()).Throws<Exception>();
    }

    [Test]
    public void when_success_return_result()
    {
        var r1 = Result<int>.Create(42);
        var r2 = r1.ThrowOnError();
        Check.That(ReferenceEquals(r1, r2)).IsTrue();
    }

    [Test]
    public void should_tap_error()
    {
        var errorTaped = false;
        var successTaped = false;
        var errorResult = ErrorResult("err1", "error");
        var resultAfterTap =
            errorResult.Tap(_ => errorTaped = true, _ => successTaped = true);

        Check.That(errorTaped).IsTrue();
        Check.That(successTaped).IsFalse();
        Check.That(errorResult).Equals(resultAfterTap);
    }

    [Test]
    public void should_tap_success()
    {
        var errorTaped = false;
        var successTaped = false;
        var errorResult = SuccessResultOf("_");
        var resultAfterTap =
            errorResult.Tap(_ => errorTaped = true, _ => successTaped = true);

        Check.That(errorTaped).IsFalse();
        Check.That(successTaped).IsTrue();
        Check.That(errorResult).Equals(resultAfterTap);
    }


    [Test]
    public void should_map_success_result_with_linq()
    {
        var x = from s in SuccessResultOf("a")
            select s.ToUpper();
        Assert.AreEqual("A", x.GetValueOrDefault());
    }

    [Test]
    public void should_map_error_result_with_linq()
    {
        var x = from s in ErrorResult("err1", "error")
            select s.ToUpper();
        Assert.AreEqual(null, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void should_combine_success_result_with_linq()
    {
        var x = from s1 in SuccessResultOf("foo")
            from s2 in SuccessResultOf("bar")
            select s1 + s2;
        Assert.AreEqual("foobar", x.GetValueOrDefault());
        Assert.IsNull(x.Error);
    }

    [Test]
    public void should_combine_error_result_with_linq()
    {
        var x = from s1 in ErrorResult("err1", "error")
            from s2 in SuccessResultOf("bar")
            select s1 + s2;
        Assert.AreEqual(null, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void should_combine_other_error_result_with_linq()
    {
        var x = from s1 in SuccessResultOf("foo")
            from s2 in ErrorResult("err1", "error")
            select s1 + s2;
        Assert.AreEqual(null, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void should_combine_multiple_error_result_with_linq()
    {
        var x = from s1 in ErrorResult("err1", "error")
            from s2 in ErrorResult("err2", "error")
            select s1 + s2;
        Assert.AreEqual(null, x.GetValueOrDefault());
        Assert.AreEqual("err1", x.Error.Content[0].key);
    }

    [Test]
    public void merge_results_when_all_are_success()
    {
        var results = new[] {SuccessResultOf("x"), SuccessResultOf("y")};
        var merged = results.Traverse();
        merged.CheckIsSuccessAnd(
            m =>
                Check.That(m).ContainsExactly("x", "y"));
    }

    [Test]
    public void merge_results_when_some_are_errors()
    {
        var results = new[]
        {
            SuccessResultOf("x"), SuccessResultOf("y"), ErrorResult("a", "a"),
            ErrorResult("b", "b")
        };
        var merged = results.Traverse();
        merged.CheckIsErrorAnd(
            m =>
                Check.That(m.Content).ContainsExactly(("a", "a"), ("b", "b")));
    }

    [Test]
    public void separate_result()
    {
        var results = new[]
        {
            SuccessResultOf("x"), 
            SuccessResultOf("y"), 
            ErrorResult("a", "a"),
            ErrorResult("b", "b")
        };

        var (errs, values) = results.SeparateResults();
        Check.That(errs).ContainsExactly(new[]
        {
            MyTestError.Create("a", "a"),
            MyTestError.Create("b", "b")
        });

        Check.That(values).ContainsExactly(new []
        {
            "x",
            "y"            
        });
    }

    private static Result<string> SuccessResultOf(string value)
    {
        return Result<string>.Create(value);
    }

    private static Result<string> ErrorResult(string errorKey, string errorMessage)
    {
        var error = MyTestError.Create(errorKey, errorMessage);
        return Result<string>.Create(error);
    }

    private Result<int> SafeTryParse(string value)
    {
        var parseSucceded = int.TryParse(value, out var result);
        if (parseSucceded) return result;

        return MyTestError.Create("parseIntError", $"{value} is not an int");
    }
}