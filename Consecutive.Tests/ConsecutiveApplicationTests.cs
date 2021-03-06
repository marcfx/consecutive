﻿using System;
using System.IO;
using System.Text;
using Consecutive.Core;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    public class ConsecutiveApplicationTests
    {
        [TestCase("24 2 5 22 8 4 3 23 22", "{2,3,4,5}\r\n{8}\r\n{22,23,24}")]
        public void ProcessTest(string input, string expected)
        {
            var cut = MakeApplication();
            string result = cut.Process(input);
            Assert.AreEqual(expected, result);
        }

        private static ConsecutiveApplication MakeApplication()
        {
            return new ConsecutiveApplication(new GroupConverter(), new ConsecutivePartitioner(), new StringWriter(new StringBuilder()));
        }

    }
}
