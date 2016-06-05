using System;
using System.IO;
using Consecutive.Core;
using Consecutive.Core.Partition.ExternalMergeSort;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Consecutive.Tests.Partition.ExternalMergeSort
{
    [TestFixture]
    public class FileSplitterTests
    {
        [Test]
        public void SplitIntoBinary()
        {
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.OpenFileForWriting(1).Returns(new FileStream("t1.binary", FileMode.Create));
            stubFileSystem.OpenFileForWriting(2).Returns(new FileStream("t2.binary", FileMode.Create));

            new FileSplitter(stubFileSystem) {NumbersPerFile = 3}.SplitUintsIntoBinaryFiles(GenerateStreamFromString("0 9 5 4 2 1"));

            using (var result1 = new BinaryReader(File.OpenRead("t1.binary")))
            {
                Assert.AreEqual(0, result1.ReadUInt32());
                Assert.AreEqual(9, result1.ReadUInt32());
                Assert.AreEqual(5, result1.ReadUInt32());
            }

            using (var result1 = new BinaryReader(File.OpenRead("t2.binary")))
            {
                Assert.AreEqual(4, result1.ReadUInt32());
                Assert.AreEqual(2, result1.ReadUInt32());
                Assert.AreEqual(1, result1.ReadUInt32());
            }
        }

        public StreamReader GenerateStreamFromString(string s)
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
