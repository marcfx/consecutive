using System;
using System.Collections.Generic;
using Consecutive.Core;
using Consecutive.Core.Partition;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Consecutive.Tests.Partition
{
    [TestFixture]
    public class ConsecutivePartitionerTest
    {
        [Test]
        public void PartitionInMemoryTest()
        {
            ConsecutivePartitioner cut = new ConsecutivePartitioner();
            IList<uint> input = new uint[] {1, 2, 3, 5, 7, 8, 9};
            IEnumerable<GroupDescriptor> result = cut.PartitionInMemory(input);
            CollectionAssert.AreEqual(new[]
            {
                new GroupDescriptor(1,3),
                new GroupDescriptor(5,1),
                new GroupDescriptor(7,3)
            }, result);
        }

        [Test]
        public void PartitionTest()
        {
            ConsecutivePartitioner cut = new ConsecutivePartitioner();
            IList<uint> input = new uint[] { 1, 2, 3, 5, 7, 8, 9, 11, uint.MaxValue-1, uint.MaxValue };
            IEnumerable<GroupDescriptor> result = cut.Partition(input);
            CollectionAssert.AreEqual(new[]
            {
                new GroupDescriptor(1,3),
                new GroupDescriptor(5,1),
                new GroupDescriptor(7,3),
                new GroupDescriptor(11,1),
                new GroupDescriptor(uint.MaxValue-1,2)
            }, result);
        }

        [Test]
        public void PartitionTest_Single()
        {
            ConsecutivePartitioner cut = new ConsecutivePartitioner();
            IList<uint> input = new uint[] { 1 };
            IEnumerable<GroupDescriptor> result = cut.Partition(input);
            CollectionAssert.AreEqual(new[]
            {
                new GroupDescriptor(1,1)
            }, result);
        }

        [Test]
        public void PartitionTest_Empty()
        {
            ConsecutivePartitioner cut = new ConsecutivePartitioner();
            IList<uint> input = new uint[] { };
            IEnumerable<GroupDescriptor> result = cut.Partition(input);
            CollectionAssert.IsEmpty(result);
        }
    }
}
