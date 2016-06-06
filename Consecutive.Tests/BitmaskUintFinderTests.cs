using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core.BigFileUniqueNumbers;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    class BitmaskUintFinderTests
    {
        [Test]
        public void FindAllUintsTest()
        {
            BitmaskUintFinder cut = new BitmaskUintFinder();
            BitArrayUint uints;
            using (StreamReader streamReader = GenerateStreamFromString("0 33 102345 99999911 2999991122"))
            {
                uints = cut.FindAllUints(streamReader);
            }
            Assert.IsTrue(uints.Get(0));
            Assert.IsTrue(uints.Get(33));
            Assert.IsTrue(uints.Get(102345));
            Assert.IsTrue(uints.Get(99999911));
            Assert.IsTrue(uints.Get(2999991122));
        }
       
        private StreamReader GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}
