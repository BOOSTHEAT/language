using System;
using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model
{
    public class AlarmNodeTest
    {
        [TestCase("C001",(ushort)1)]
        [TestCase("C012",(ushort)12)]
        [TestCase("C123",(ushort)123)]
        public void it_respects_naming_convention(string tokenName, ushort expectedAlarmNumber)
        {
            var node = new AlarmNode(tokenName, null);
            Check.That(node.number).IsEqualTo(expectedAlarmNumber);
        }

        [TestCase("C0")]
        [TestCase("C00")]
        [TestCase("D001")]
        [TestCase("C0010")]
        [TestCase("C001X")]
        public void it_doesnt_respect_naming_convention(string token)
        {
            Check.ThatCode(() => new AlarmNode(token, null)).Throws<ArgumentException>();
        }
    }
}