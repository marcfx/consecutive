using System;
using System.Collections.Generic;
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
            IEnumerable<uint> numbers = _groupConverter.Parse(input);
            IList<uint> numbersInMemory = numbers.ToArray();
            var partitionResult = _consecutivePartitioner.Partition(numbersInMemory);
            //_groupConverter.Convert()
            return null;
        }
    }
}
