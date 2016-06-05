using System;
using System.Collections.Generic;
using Consecutive.Core;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Consecutive.Tests
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
