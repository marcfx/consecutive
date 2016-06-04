using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public interface IConsecutivePartitioner
    {
        IEnumerable<GroupDescriptor> Partition(IList<uint> values);
    }
}