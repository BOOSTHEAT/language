using ImpliciX.Language.Core;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Core
{   
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void some_should_accept_nonnull_values()
        {
            var option = Option<string>.Some("a");
            Assert.AreEqual("a",option.GetValueOrDefault(""));
            Assert.IsTrue(option.IsSome);
        }

        [Test]
        public void none_should_not_be_some()
        {
            var option = Option<string>.None();
            Assert.IsFalse(option.IsSome);
        }

        [Test]
        public void should_execute_whenNone()
        {
            var option = Option<string>.None();
            var result = option.Match(whenNone: () => "none", whenSome:(value) => "some");
            Assert.AreEqual("none", result);
        }

        [Test]
        public void should_execute_whenSome()
        {
            var option = Option<string>.Some("awe");
            var result = option.Match(whenNone: () => "none", whenSome: (value) => value+"some");
            Assert.AreEqual("awesome", result);
        }

        [Test]
        public void should_create_some()
        {
            var option = 100.ToOption();
            Assert.IsTrue(option.IsSome);
            Assert.AreEqual(100,option.GetValue());
        }

        [Test]
        public void should_create_none()
        {
            var option = default(object).ToOption();
            Assert.IsFalse(option.IsSome);
        }

        [Test]
        public void map_some()
        {
            var result = 100.ToOption().Map((v) => $"value is {v}");
            Assert.AreEqual("value is 100",result.GetValue());
        }

        [Test]
        public void map_nothing_should_not_throw_errors()
        {
            default(object).ToOption().Map(m => m.ToString());
        }

        [Test]
        public void bind_some()
        {
            var result = 100.ToOption().Bind((v) => $"value is {v}".ToOption());
            Assert.AreEqual("value is 100", result.GetValue());
        }

        [Test]
        public void bind_nothing_should_not_throw_errors()
        {
            default(object).ToOption().Bind(m => m.ToString().ToOption());
        }
        
        [Test]
        public void should_map_some_option_with_linq()
        {
            var x = from s in "a".ToOption()
                select s.ToUpper();
            Assert.AreEqual("A", x.GetValue());
        }
        
        [Test]
        public void should_map_none_option_with_linq()
        {
            var x = from s in default(string).ToOption()
                select s.ToUpper();
            Assert.AreEqual(null, x.GetValue());
        }
        
        [Test]
        public void should_combine_some_with_some_options_with_linq()
        {
            var x = from s1 in "foo".ToOption()
                from s2 in "bar".ToOption()
                select s1 + s2;
            Assert.AreEqual("foobar", x.GetValue());
        }
        
        [Test]
        public void should_combine_none_with_some_with_linq()
        {
            var x = from s1 in default(string).ToOption()
                from s2 in "bar".ToOption()
                select s1 + s2;
            Assert.AreEqual(null, x.GetValue());
        }
        
        [Test]
        public void should_combine_some_with_nome_with_linq()
        {
            var x = from s1 in "foo".ToOption()
                from s2 in default(string).ToOption()
                select s1 + s2;
            Assert.AreEqual(null, x.GetValue());
        }

        [Test]
        public void should_combine_none_with_none_options_with_linq()
        {
            var x = from s1 in default(string).ToOption()
                from s2 in default(string).ToOption()
                select s1 + s2;
            Assert.AreEqual(null, x.GetValue());
        }


        
        [Test]
        public void should_be_equal_some()
        {
            var o1 = "ABC".ToOption();
            var o2 = "ABC".ToOption();
            Assert.IsTrue(o1.Equals(o2));
        }
        
        [Test]
        public void should_be_equal_none()
        {
            var o1 = default(string).ToOption();
            var o2 = default(string).ToOption();
            Assert.IsTrue(o1.Equals(o2));
        }
        
        
        [Test]
        public void should_not_be_equal_some()
        {
            var o1 = "ABC".ToOption();
            var o2 = "ABCD".ToOption();
            Assert.IsFalse(o1.Equals(o2));
        }
        
         
        [Test]
        public void should_not_be_equal_some_none()
        {
            var o1 = default(string).ToOption();
            var o2 = "ABCD".ToOption();
            Assert.IsFalse(o1.Equals(o2));
        }

        [Test]
        public void should_not_be_equal_some_none_default_values()
        {
            var o1 = Option<int>.None();
            var o2 = Option<int>.Some(0);
            Assert.IsFalse(o1.Equals(o2));
        }
    }
}