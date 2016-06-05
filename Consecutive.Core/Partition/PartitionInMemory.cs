using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public class PartitionInMemory : IConsecutivePartitioner
    {
        public IEnumerable<GroupDescriptor> Partition(IList<uint> sorted)
        {
            if (sorted == null || sorted.Count == 0)
            {
                yield break;
            }

            int startIndex = 0;
            for (int i = 1; i < sorted.Count; i++)
            {
                if (IsConsecutive(sorted, i))
                {
                    continue;
                }
                yield return new GroupDescriptor(sorted[startIndex], i - startIndex); 
                startIndex = i;
            }

            yield return new GroupDescriptor(sorted[startIndex], sorted.Count - startIndex);
        }

        private static bool IsConsecutive(IList<uint> values, int currentIndex)
        {
            int previousIndex = currentIndex - 1;
            return values[currentIndex] == values[previousIndex] + 1;
        }
    }
}
