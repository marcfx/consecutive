using System.Collections.Generic;
using Consecutive.Core.Partition;
using NUnit.Framework;

namespace Consecutive.Tests.Partition
{
    [TestFixture]
    public class GroupDescriptorTests
    {
        [Test]
        public void GetSequenceTest()
        {
            //Arange
            var cut = new GroupDescriptor(3,4);
            //Act
            IEnumerable<uint> result = cut.GetSequence();
            //Assert
            CollectionAssert.AreEqual(new uint[] {3,4,5,6}, result);
        }

    }
}
