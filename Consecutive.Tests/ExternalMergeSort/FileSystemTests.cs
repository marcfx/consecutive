using System.Collections.Generic;
using System.IO;
using Consecutive.Core.ExternalMergeSort;
using NUnit.Framework;

namespace Consecutive.Tests.ExternalMergeSort
{
    [TestFixture]
    class FileSystemTests
    {
        [Test]
        public void ReadFileIntoMemoryTest()
        {
            using (var writer = new BinaryWriter(new FileStream("test1.binary", FileMode.Create)))
            {
                uint a = 13;
                uint b = 33;
                writer.Write(a);
                writer.Write(b);
            }

            var fileSystem = new FileSystem {FilePattern = "test{0}.binary"};
            IList<uint> result = fileSystem.ReadFileIntoMemory(1);

            CollectionAssert.AreEqual(new uint[] {13,33}, result);
        }
    }
}
