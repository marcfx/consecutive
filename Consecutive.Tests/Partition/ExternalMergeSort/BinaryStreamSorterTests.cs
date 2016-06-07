using System.Collections.Generic;
using System.IO;
using Consecutive.Core.BigFileSorting;
using NUnit.Framework;

namespace Consecutive.Tests.Partition.ExternalMergeSort
{
    [TestFixture]
    class BinaryStreamSorterTests
    {
        [Test]
        public void SortBinaryFileInMemory()
        {
            Stream stream = GenerateStreamFromString(new List<uint> {3, 2, 1, 2, 7});
            new BinaryStreamSorter().SortBinaryFileInMemory(stream);
            stream.Position = 0;
            BinaryReader br = new BinaryReader(stream);
            CollectionAssert.AreEqual(
                new uint[] {1,2,2,3,7}, 
                new[] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() });
        }

        private static Stream GenerateStreamFromString(IEnumerable<uint> numbers)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            foreach (uint number in numbers)
            {
                writer.Write(number);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
