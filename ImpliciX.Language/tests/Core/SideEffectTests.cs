using System;
using System.Diagnostics;
using ImpliciX.Language.Core;
using NFluent;
using NUnit.Framework;
using static ImpliciX.Language.Core.SideEffect;

namespace ImpliciX.Language.Tests.Core
{
    [TestFixture]
    public class SideEffectTests
    {
        private Result<string> _testTryRun(bool withDefault, Func<string> f, int retryCount = 0, int retryTimeout = 0)
        {
            return withDefault switch
            {
                false => TryRun(f, FooError.Create, RetryPolicy.Create(retryCount, retryTimeout)),
                true => TryRunOrDefault(f, e => "I am default", RetryPolicy.Create(retryCount, retryTimeout))
            };
        }

        [TestCase(true)]
        [TestCase(false)]
        public void when_no_exception_is_raised_returns_a_success_result(bool withDefault)
        {
            var result = _testTryRun(withDefault, () => "1" + "1");
            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.GetValueOrDefault()).IsEqualTo("11");
        }

        [Test]
        public void when_an_exception_is_raised_returns_an_error_result_and_do_not_rethrows_the_exception()
        {
            var result = TryRun<int>(() => throw new Exception("boom"), FooError.Create);
            Check.That(result.IsError).IsTrue();
            Check.That(result.Error).IsInstanceOf<FooError>();
        }

        [Test]
        public void try_run_get_default()
        {
            var result = TryRunOrDefault(() => throw new Exception("boom"), e => "I am default");
            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.Value).IsEqualTo("I am default");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void retry_policy(bool withDefault)
        {
            var count = 0;
           _testTryRun(withDefault, () =>
            {
                count++;
                throw new Exception("boom");
            }, 3);
            Check.That(count).IsEqualTo(4);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void if_succeeds_first_time_try_only_once(bool withDefault)
        {
            var count = 0;
            _testTryRun(withDefault, () =>
            {
                count++;
                return "test";
            },  3);
            Check.That(count == 1);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void retry_policy_with_delay(bool withDefault)
        {
            var count = 0;
            var stopwatch = Stopwatch.StartNew();

           _testTryRun(withDefault, () =>
            {
                count++;
                throw new Exception("boom");
            }, 3, 100);

            stopwatch.Stop();

            Check.That(stopwatch.ElapsedMilliseconds).IsStrictlyGreaterThan(199);
            Check.That(count).IsEqualTo(4);
        }
        
    }

    public class FooError : Error
    {
        public static FooError Create()
        {
            return new FooError();
        }

        private FooError() : base(nameof(FooError), nameof(FooError))
        {
        }
    }
}