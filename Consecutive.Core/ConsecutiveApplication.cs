using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core.BigFileUniqueNumbers;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
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
            var bigArray = new BitmaskUintFinder();
            BitArrayUint uints;
            using (var stream = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                uints = bigArray.FindAllUints(stream);
            }
            IEnumerable<GroupDescriptor> consecutiveGroups = _consecutivePartitioner.Partition(uints.GetValuesFromArray());
            WriteToFile(outWriter, consecutiveGroups);
        }

        private void WriteToFile(TextWriter outWriter, IEnumerable<GroupDescriptor> consecutiveGroups)
        {
            using (var pbar = new ProgressBar(3000, "Writing groups", ConsoleColor.Cyan))
            {
                foreach (GroupDescriptor groupDescriptor in consecutiveGroups)
                {
                    if (groupDescriptor.ConsecutiveStart%1000000 < 100)
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
