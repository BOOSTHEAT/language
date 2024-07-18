using ImpliciX.Language.Model;
using NFluent;
using NUnit.Framework;

namespace ImpliciX.Language.Tests.Model
{
  [TestFixture]
  public class DurationTests
  {
    [Test]
    public void MillisecondsPrecision()
    {
      var d = Duration.FromFloat(1.526f);
      Check.That(d.Value.Milliseconds).IsEqualTo(1526);
    }
  }
}