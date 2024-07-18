using System;
using ImpliciX.Language.Core;
using NFluent;
using NUnit.Framework;
namespace ImpliciX.Language.Tests.Core
{
    [TestFixture]
    public class ResultTests
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
            var x = ItReturnsErrorResult("err1", "error")
                .Match(whenError: err => err.Content[0].value, whenSuccess: val => "success");
            Assert.AreEqual("error", x);
        }

        [Test]
        public void should_match_success_result()
        {
            var x = ItReturnsSuccessResultOf("success")
                .Match(
                    whenError: err => err.Content[0].value,
                    whenSuccess: val => "success"
                );
            Assert.AreEqual("success", x);
        }



        [Test]
        public void should_return_the_value()
        {
            const string DefaultValue = "default value";
            var valueFromError = ItReturnsErrorResult("err1", "error").GetValueOrDefault(DefaultValue);
            var valueFromSuccess = ItReturnsSuccessResultOf("my value").GetValueOrDefault(DefaultValue);
            Assert.AreEqual(valueFromError, DefaultValue);
            Assert.AreEqual(valueFromSuccess, "my value");
        }



        [Test]
        public void should_be_equal_when_have_the_same_value()
        {
            var r1 = Result<string>.Create("a");
            var r2 = Result<string>.Create("a");
            Assert.IsTrue(r1.Equals(r2));
        }
        
        [Test]
        public void should_not_be_equal_when_do_not_have_the_same_value()
        {
            var r1 = Result<string>.Create("a");
            var r2 = Result<string>.Create("b");
            Assert.IsFalse(r1.Equals(r2));
        }
        
        [Test]
        public void should_be_equal_when_have_the_same_error()
        {
            var r1 = Result<string>.Create(new Error("key","message"));
            var r2 = Result<string>.Create(new Error("key","message"));
            Assert.IsTrue(r1.Equals(r2));
            
        }
        
        [Test]
        public void should_not_be_equal_when_dont_have_the_same_error()
        {
            var r1 = Result<string>.Create(new Error("key","message"));
            var r2 = Result<string>.Create(new Error("key1","message"));
            Assert.IsFalse(r1.Equals(r2));
        }
        
        
        [Test]
        public void should_be_equal_when_have_the_same_equatable_value()
        {
            var r1 = Result<Qix>.Create(new Qix("A",1));
            var r2 = Result<Qix>.Create(new Qix("A",1));
            Assert.IsTrue(r1.Equals(r2));
        }
        
        [Test]
        public void should_not_be_equal_when_do_not_have_the_same_equatable_value()
        {
            var r1 = Result<Qix>.Create(new Qix("A",1));
            var r2 = Result<Qix>.Create(new Qix("A",2));
            Assert.IsFalse(r1.Equals(r2));
        }
        
        [Test]
        public void should_not_be_equal_when_do_not_have_the_same_equatable_value_types()
        {
            var r1 = Result<Pix>.Create(new Pix("A",1));
            var r2 = Result<Qix>.Create(new Qix("A",1));
            Assert.IsFalse(r1.Equals(r2));
        }
        
        [Test]
        public void should_be_equal_when_have_the_same_equatable_value_lifted_not_lifted()
        {
            var lifted = Result<Qix>.Create(new Qix("A",1));
            var notLifted = new Qix("A",1);
            Assert.IsTrue(lifted.Equals(notLifted));
        }
        
        [Test]
        public void should_not_be_equal_error_and_success()
        {
            var r1 = Result<int>.Create(new Error("key","message"));
            var r2 = Result<int>.Create(0);
            Assert.IsFalse(r1.Equals(r2));
        }
        
        [Test]
        public void should_not_be_equal_inheritace()
        {
            var r1 = Result<SuperBizz>.Create(new Bizz("A", 1, 2));
            var r2 = Result<SuperBizz>.Create(new BizzPrim("A",1,2));
            Assert.IsFalse(r1.Equals(r2));
        }
        


        private static Result<string> ItReturnsSuccessResultOf(string value)
        {
            return Result<string>.Create(value);
        }

        private static Result<string> ItReturnsErrorResult(string errorKey, string errorMessage)
        {
            var error = MyTestError.Create(errorKey, errorMessage);
            return Result<string>.Create(error);
        }
    }

    public class SuperBizz : IEquatable<SuperBizz>
    {
        public string A { get; }
        public int B { get; }

        public SuperBizz(string a, int b)
        {
            A = a;
            B = b;
        }

        public bool Equals(SuperBizz other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return A == other.A && B == other.B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SuperBizz) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }

    public class Bizz : SuperBizz, IEquatable<Bizz>
    {
        public int C { get; }

        public Bizz(string a, int b, int c) : base(a, b)
        {
            C = c;
        }

        public bool Equals(Bizz other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && C == other.C;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bizz) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), C);
        }
    }
    
    public class BizzPrim : SuperBizz, IEquatable<BizzPrim>
    {
        public int D { get; }

        public BizzPrim(string a, int b, int d) : base(a, b)
        {
            D = d;
        }

        public bool Equals(BizzPrim other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && D == other.D;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BizzPrim) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), D);
        }
    }

    public class Pix : IEquatable<Pix>
    {
        public string A { get; }
        public int B { get; }

        public Pix(string a, int b)
        {
            A = a;
            B = b;
        }

        public bool Equals(Pix other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return A == other.A && B == other.B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pix) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }
    
    public class Qix : IEquatable<Qix>
    {
        public string A { get; }
        public int B { get; }

        public Qix(string a, int b)
        {
            A = a;
            B = b;
        }

        public bool Equals(Qix other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return A == other.A && B == other.B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Qix) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }

    public class MyTestError : Error
    {
        public static MyTestError Create(string key, string message)
        {
            return new MyTestError(key, message);
        }

        private MyTestError(string key, string message) : base(key, message)
        {
        }
    }
}