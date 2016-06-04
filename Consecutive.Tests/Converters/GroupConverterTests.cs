using System;
using System.Collections.Generic;
using Consecutive.Core;
using Consecutive.Core.Converters;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Consecutive.Tests.Converters
{
    [TestFixture]
    public class GroupConverterTests
    {
        [Test]
        public void Convert_SingleDimension_ToString()
        {
            //Arange
            var input = new uint[] {1, 3, 3, 8, 1};
            IGroupConverter converter = new GroupConverter();
            //Act
            string result = converter.Convert(input);
            //Assert
            Assert.AreEqual("{1,3,3,8,1}", result);
        }

        [Test]
        public void Convert_TwoDimension_ToString()
        {
            //Arange
            var input = new[] { new GroupDescriptor(3,2), new GroupDescriptor(4,2)};
            IGroupConverter converter = new GroupConverter();
            //Act
            string result = converter.Convert(input);
            //Assert
            Assert.AreEqual("{3,4},{4,5}", result);
        }

        [Test]
        public void Parse_OneDimension_ToString()
        {
            //Arange
            string input = "{1,3,3,8,1}";
            IGroupConverter converter = new GroupConverter();
            //Act
            IEnumerable<uint> result = converter.Parse(input);
            //Assert
            CollectionAssert.AreEqual(new uint[] { 1, 3, 3, 8, 1 }, result);
        }

    }
}
