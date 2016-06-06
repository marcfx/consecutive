using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core.BigFileUniqueNumbers;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    class BigArrayUintTests
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
            var cut = new BitArrayUint();
            cut.Set(index, value);
            Assert.IsTrue(expected == cut.Get(checkIndex));
        }

        [Test]
        
        public void Test()
        {
            BitArray ba = new BitArray(int.MaxValue);
            ba.Set(int.MaxValue-1, true);
            Assert.IsTrue(ba.Get(int.MaxValue-1));
        }

    }
}
