using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Consecutive.Core.BigFileSorting;
using Consecutive.Core.BigFileUniqueNumbers;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
using Consecutive.Core.ProgressBar;
using ShellProgressBar;

namespace Consecutive.Core
{
    public class ConsecutiveApplication 
    {
        private readonly IGroupConverter _groupConverter;
        private readonly IConsecutivePartitioner _consecutivePartitioner;
        private readonly TextWriter _progressReporter;

        public ConsecutiveApplication(IGroupConverter groupConverter, IConsecutivePartitioner consecutivePartitioner, TextWriter progressReporter)
        {
            _groupConverter = groupConverter;
            _consecutivePartitioner = consecutivePartitioner;
            _progressReporter = progressReporter;
        }

        public string Process(string input)
        {
            IEnumerable<uint> numbersRaw = _groupConverter.Parse(input);
            IEnumerable<uint> numbersSorted = numbersRaw.Distinct().OrderBy(n => n);
            IList<uint> numbersInMemory = numbersSorted.ToArray();
            IEnumerable<GroupDescriptor> partitionResult = _consecutivePartitioner.Partition(numbersInMemory);
            return _groupConverter.Convert(partitionResult);
        }

        public void ProcessFile(string fileName, TextWriter outWriter)
        {
            var bigArray = new BitmaskUIntFinder(new Progress());
            BitArrayUInt uInts;
            using (var stream = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                uInts = bigArray.FindAllUInts(stream);
            }
            IEnumerable<GroupDescriptor> consecutiveGroups = _consecutivePartitioner.Partition(uInts.GetValuesFromArray());
            WriteToFile(outWriter, consecutiveGroups);
        }

        public void ProcessMergeSort(string fileName, TextWriter outWriter)
        {
            var fileSorter = Bootstrap();
            IEnumerable<uint> uIntIterator = fileSorter.Sort(fileName);
            IEnumerable<GroupDescriptor> consecutiveGroups = _consecutivePartitioner.Partition(uIntIterator);
            WriteToFile(outWriter, consecutiveGroups);
        }

        private static BigFileSorter Bootstrap()
        {
            var fileSystem = new FileSystem();
            return new BigFileSorter(new FileSplitter(fileSystem, new Progress()), fileSystem, new BinaryStreamSorter(), new FilePartMerger(fileSystem));
        }

        private void WriteToFile(TextWriter outWriter, IEnumerable<GroupDescriptor> consecutiveGroups)
        {
            using (var pbar = new ShellProgressBar.ProgressBar(3000, "Writing groups", ConsoleColor.Cyan))
            {
                int i = 0;
                foreach (GroupDescriptor groupDescriptor in consecutiveGroups)
                {
                    if (i++%500000 ==0)
                    {
                        pbar.Tick("Writing groups " + groupDescriptor.ConsecutiveStart);
                    }
                    
                    string formatedSequence = _groupConverter.Convert(groupDescriptor.GetSequence());
                    outWriter.WriteLine(formatedSequence);
                }
            }
        }
    }
}
