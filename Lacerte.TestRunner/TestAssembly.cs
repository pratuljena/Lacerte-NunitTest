using NUnit.Framework;

namespace Lacerte.TestRunner
{
    [TestFixture]
    class TestAssembly
    {
        [Test]
        public void ShoudAdd()
        {
            Assert.AreEqual(3 + 3, 6);
        }
        [Test]
        public void ShoudSubtract()
        {
            Assert.AreEqual(3 - 3, 0);
        }
        [Test]
        public void ShoudMultiply()
        {
            Assert.AreEqual(3 * 3, 6);
        }
        [Test]
        public void ShoudDivide()
        {
            Assert.AreEqual(3 / 3, 1);
        }
    }
}
