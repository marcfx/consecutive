using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public class PartitionInMemory : IConsecutivePartitioner
    {
        public IEnumerable<GroupDescriptor> Partition(IList<uint> values)
        {
            if (values == null || values.Count == 0)
            {
                yield break;
            }

            int startIndex = 0;
            for (int i = 1; i < values.Count; i++)
            {
                if (IsConsecutive(values, i))
                {
                    continue;
                }
                yield return new GroupDescriptor(values[startIndex], i - startIndex); 
                startIndex = i;
            }

            yield return new GroupDescriptor(values[startIndex], values.Count - startIndex);
        }

        private static bool IsConsecutive(IList<uint> values, int currentIndex)
        {
            int previousIndex = currentIndex - 1;
            return values[currentIndex] == values[previousIndex] + 1;
        }
    }
}
