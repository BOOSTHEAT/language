using System;
using ImpliciX.Language.Core;
using NFluent;
using NUnit.Framework;
namespace ImpliciX.Language.Tests.Core
{
    [TestFixture]
    public class Result2Tests
    {
        [Test]
        public void should_create_error_result()
        {
            var error = MyTestError.Create("err1", "error");
            var result = Result<string>.Create(error);
            Assert.IsTrue(result.IsError);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.GetValueOrDefault());
            Assert.AreEqual(error, result.Error);
        }

        [Test]
        public void should_create_value_result()
        {
            var result = Result<string>.Create("success");
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("success", result.GetValueOrDefault());
            Assert.IsNull(result.Error);
        }

        [Test]
        public void should_match_error_result()
        {
            var x = ItReturnsErrorResult("err1", "error",42)
                .Match(
                    whenError: (err,b) => err.Content[0].value+b, 
                    whenSuccess: (val,b) => "success"+b);
            Assert.AreEqual("error42", x);
        }

        [Test]
        public void should_match_success_result()
        {
            var x = ItReturnsSuccessResultOf("success",42)
                .Match(
                    whenError: (err,b) => err.Content[0].value + b,
                    whenSuccess: (val,b) => "success" + b
                );
            Assert.AreEqual("success42", x);
        }
        
        [Test]
        public void should_map_success_result()
        {
            var x = ItReturnsSuccessResultOf("a",0).Map((s => (s.Item1.ToUpper()+s.Item2,s.Item2+1)));
            Assert.AreEqual("A0", x.GetValueOrDefault());
            Assert.AreEqual(1, x.Both);
        }

        [Test]
        public void should_map_error_result()
        {
            var x = ItReturnsErrorResult("err1", "error",0)
                .Map((s => (s.Item1.ToUpper(),s.Item2+1)));
            Assert.AreEqual(null, x.GetValueOrDefault());
            Assert.AreEqual("err1", x.Error.Content[0].key);
            Assert.AreEqual(0, x.Both);
        }
        
        [Test]
        public void should_map_success_result_with_linq()
        {
            var x =
                from s in ItReturnsSuccessResultOf("a", 0)
                select (s.Item1.ToUpper() + s.Item2, s.Item2+1);
            Assert.AreEqual("A0", x.GetValueOrDefault());
            Assert.AreEqual(1, x.Both);
        }
        
        [Test]
        public void should_map_error_result_with_linq()
        {
            var x = from s in ItReturnsErrorResult("err1", "error",0)
                select (s.value.ToUpper(), s.both);
            Assert.AreEqual(null, x.GetValueOrDefault());
            Assert.AreEqual("err1", x.Error.Content[0].key);
        }
        
        [Test]
        public void should_combine_success_result_with_linq()
        {
            var x = 
                from s1 in ItReturnsSuccessResultOf("foo",1)
                from s2 in ItReturnsSuccessResultOf2("1",2)
                select (s1.Item1 + s2.Item1,s1.Item2+s2.Item2);
            Assert.AreEqual("foo1", x.GetValueOrDefault());
            Assert.AreEqual(3, x.Both);
            Assert.IsNull(x.Error);
        }
        
        
        [Test]
        public void when_error_throw_exception()
        {
            var r1 = Result2<int,string>.Create(new SomeError("error"),"boo");
            Check.ThatCode(() => r1.ThrowOnError()).Throws<Exception>();
        }
    
        [Test]
        public void when_success_return_result()
        {
            var r1 = Result2<int,string>.Create(42,"foo");
            var r2 = r1.ThrowOnError();
            Check.That(ReferenceEquals(r1,r2)).IsTrue();
        }
        
        private static Result2<string,int> ItReturnsSuccessResultOf(string v, int b)
        {
            return Result2<string,int>.Create(v,b);
        }
        
        private static Result2<int,int> ItReturnsSuccessResultOf2(string v, int b)
        {
            return Result2<int,int>.Create(int.Parse(v),b);
        }

        private static Result2<string,int> ItReturnsErrorResult(string errorKey, string errorMessage, int b)
        {
            var error = MyTestError.Create(errorKey, errorMessage);
            return Result2<string,int>.Create(error, b);
        }
        
        
    }
}