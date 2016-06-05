using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;

namespace Consecutive.Core
{
    public class ConsecutiveApplication 
    {
        private readonly IGroupConverter _groupConverter;
        private readonly IConsecutivePartitioner _consecutivePartitioner;

        public ConsecutiveApplication(IGroupConverter groupConverter, IConsecutivePartitioner consecutivePartitioner)
        {
            _groupConverter = groupConverter;
            _consecutivePartitioner = consecutivePartitioner;
        }

        public string Process(string input)
        {
            IEnumerable<uint> numbersRaw = _groupConverter.Parse(input);
            IEnumerable<uint> numbersSorted = numbersRaw.Distinct().OrderBy(n => n);
            IList<uint> numbersInMemory = numbersSorted.ToArray();
            IEnumerable<GroupDescriptor> partitionResult = _consecutivePartitioner.Partition(numbersInMemory);
            return _groupConverter.Convert(partitionResult);
        }

        //public StreamWriter Process(StreamReader streamReader)
        //{
        //    //IEnumerable<uint> numbersRaw = _groupConverter.Parse(input);
        //    //IEnumerable<uint> numbersSorted = numbersRaw.Distinct().OrderBy(n => n);
        //    //IList<uint> numbersInMemory = numbersSorted.ToArray();
        //    //IEnumerable<GroupDescriptor> partitionResult = _consecutivePartitioner.Partition(numbersInMemory);
        //    //return _groupConverter.Convert(partitionResult);
        //}
    }
}
