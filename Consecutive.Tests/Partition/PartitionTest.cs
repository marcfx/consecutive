using System;
using System.Collections.Generic;
using Consecutive.Core;
using Consecutive.Core.Partition;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Consecutive.Tests.Partition
{
    [TestFixture]
    public class PartitionInMemoryTest
    {
        [Test]
        public void PartitionTest()
        {
            PartitionInMemory cut = new PartitionInMemory();
            IList<uint> input = new uint[] {1, 2, 3, 5, 7, 8, 9};
            IEnumerable<GroupDescriptor> result = cut.Partition(input);
            CollectionAssert.AreEqual(new[]
            {
                new GroupDescriptor(1,3),
                new GroupDescriptor(5,1),
                new GroupDescriptor(7,3)
            }, result);
        }
    }
}
