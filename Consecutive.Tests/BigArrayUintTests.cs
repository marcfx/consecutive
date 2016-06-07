using Consecutive.Core.BigFileUniqueNumbers;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    class BigArrayUIntTests
    {
        [TestCase(0U, true, 0U, true)]
        [TestCase(1U, true, 1U, true)]
        [TestCase(uint.MaxValue, true, uint.MaxValue, true)]
        [TestCase(uint.MaxValue-1, true, uint.MaxValue-1, true)]
        [TestCase((uint)int.MaxValue-1, true, (uint)int.MaxValue-1, true)]
        [TestCase((uint)int.MaxValue, true, (uint)int.MaxValue, true)]
        [TestCase((uint)int.MaxValue+1, true, (uint)int.MaxValue+1, true)]
        public void GetSetTest(uint index, bool value, uint checkIndex, bool expected)
        {
            var cut = new BitArrayUInt();
            cut.Set(index, value);
            Assert.IsTrue(expected == cut.Get(checkIndex));
        }

        [Test]
        
        public void GetValuesFromArray()
        {
            var cut = new BitArrayUInt();
            cut.Set(1, true);
            cut.Set(11, true);
            cut.Set(11111, true);
            cut.Set(100000000, true);
            cut.Set(4000000000, true);
            CollectionAssert.AreEqual(
                new uint[] {1,11,11111, 100000000, 4000000000 }, cut.GetValuesFromArray());
        }

    }
}
