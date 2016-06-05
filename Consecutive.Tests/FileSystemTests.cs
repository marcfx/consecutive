using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core;
using NUnit.Framework;

namespace Consecutive.Tests
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
