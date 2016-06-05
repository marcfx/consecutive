using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core;
using Consecutive.Core.BigFileSorting;
using NUnit.Framework;

namespace Consecutive.Tests.Partition.ExternalMergeSort
{
    [TestFixture]
    class BigFileSorterTests
    {
        [Test]
        public void SortBinaryFileInMemory()
        {
            BigFileSorter sorter = MakeSorter();
            sorter.Sort(@"c:\Users\Marek\Source\Repos\consecutive\Consecutive.Console\bin\Debug\Test2.txt");
        }

        private static BigFileSorter MakeSorter()
        {
            var fs = new FileSystem();
            return new BigFileSorter(new FileSplitter(fs), fs, new BinaryStreamSorter(), new FilePartMerger(fs) );
        }

        
    }
}
