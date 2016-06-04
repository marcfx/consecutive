using System;
using Consecutive.Core;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    public class ConsecutivePartitionerIntegrationTests
    {
        [TestCase("24,2,5,22,8,4,3,23,22", "{2,3,4,5},{8},{22,23,24}")]
        public void ProcessTest(string input, string expected)
        {
            var target = new ConsecutiveApplication(new GroupConverter(), new PartitionInMemory());
            string result = target.Process(input);
            Assert.AreEqual(expected, result);
        }
    }
}
