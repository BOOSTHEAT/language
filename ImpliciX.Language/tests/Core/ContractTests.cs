using ImpliciX.Language.Core;
using NUnit.Framework;
namespace ImpliciX.Language.Tests.Core
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        [Category("ExcludeFromCI")]
        public void should_raise_an_contract_exception()
        {
            int input = -1;
            Assert.Throws<ContractException>(() => LContract.PreCondition(()=>input >= 0, ()=>"Input should be a positive integer."));
        }

        [Test]
        public void should_pass_contract_validation()
        {
            int input = 0;
            Assert.DoesNotThrow(()=>LContract.PreCondition(()=>input >= 0, ()=>"Input should be a positive integer."));
        }
    }

 
}
