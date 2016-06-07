using System.Collections.Generic;
using System.IO;
using Consecutive.Core.ExternalMergeSort;
using NUnit.Framework;

namespace Consecutive.Tests.ExternalMergeSort
{
    [TestFixture()]
    class FilePartMergerTests
    {
        [Test]
        public void MergeSortPartsTest()
        {
            using (var writer = new BinaryWriter(new FileStream("test0.binary", FileMode.Create)))
            {
                writer.Write((uint)11);
                writer.Write((uint)33);
            }

            using (var writer = new BinaryWriter(new FileStream("test1.binary", FileMode.Create)))
            {
                writer.Write((uint)22);
                writer.Write((uint)44);
            }


            IEnumerable<uint> values = new FilePartMerger(new FileSystem {FilePattern = "test{0}.binary"}).MergeSortParts(2);

            CollectionAssert.AreEqual(new uint[] {11,22,33,44}, values);
        }
    }
}
