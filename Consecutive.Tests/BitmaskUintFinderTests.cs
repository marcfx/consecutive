using System.IO;
using Consecutive.Core.BigFileUniqueNumbers;
using Consecutive.Core.ProgressBar;
using NSubstitute;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    class BitmaskUIntFinderTests
    {
        [Test]
        public void FindAllUIntsTest()
        {
            BitmaskUIntFinder cut = new BitmaskUIntFinder(Substitute.For<IProgress>());
            BitArrayUInt uInts;
            using (StreamReader streamReader = GenerateStreamFromString("0 33 102345 99999911 2999991122"))
            {
                uInts = cut.FindAllUInts(streamReader);
            }
            Assert.IsTrue(uInts.Get(0));
            Assert.IsTrue(uInts.Get(33));
            Assert.IsTrue(uInts.Get(102345));
            Assert.IsTrue(uInts.Get(99999911));
            Assert.IsTrue(uInts.Get(2999991122));
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
