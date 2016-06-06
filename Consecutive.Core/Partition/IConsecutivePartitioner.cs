using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public interface IConsecutivePartitioner
    {
        IEnumerable<GroupDescriptor> PartitionInMemory(IList<uint> sorted);

        IEnumerable<GroupDescriptor> Partition(IEnumerable<uint> sorted);
    }
}