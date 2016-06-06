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
    class BitmaskUitFinderTests
    {
        [Test]
        public void FindAllUintsTest()
        {
            BitmaskUitFinder cut = new BitmaskUitFinder();
            BitArray uints;
            using (StreamReader streamReader = GenerateStreamFromString("0 33 102345 99999911"))
            {
                uints = cut.FindAllUints(streamReader);
            }
            Assert.IsTrue(uints[0]);
            Assert.IsTrue(uints[33]);
            Assert.IsTrue(uints[102345]);
            Assert.IsTrue(uints[99999911]);

        }

        [Test]
        public void StoreUint()
        {
            int intInUintMax = (int)(uint.MaxValue - int.MaxValue-1);
            var intInUintMin = (int)uint.MinValue - int.MaxValue;

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
