using NUnit.Framework;

namespace GiamminLib.Tests
{
    [TestFixture]
    public class FailTest
    {

        [Test]
        public void Fail(string clearString)
        {
            Assert.Fail( "this test is meant to fail");
        }
    }
}
