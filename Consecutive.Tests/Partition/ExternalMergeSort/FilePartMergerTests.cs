using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core;
using Consecutive.Core.Partition.ExternalMergeSort;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Consecutive.Tests.Partition.ExternalMergeSort
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


            new FilePartMerger(new FileSystem {FilePattern = "test{0}.binary"}).MergeSortParts(2, "c:/Test.txt");       
        }
    }
}
