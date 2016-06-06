using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public class ConsecutivePartitioner : IConsecutivePartitioner
    {
        public IEnumerable<GroupDescriptor> PartitionInMemory(IList<uint> sorted)
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

        private static bool IsConsecutive(uint current, uint previous)
        {
            return current == previous + 1;
        }

        public IEnumerable<GroupDescriptor> Partition(IEnumerable<uint> sorted)
        {
            if (sorted == null)
            {
                yield break;
            }

            int consecutiveCount = 1;
            uint? previous = null;
            uint? consecutiveStart=null;
            foreach (uint current in sorted)
            {
                if (consecutiveStart == null)
                {
                    consecutiveStart = current;
                }
                if (previous == null)
                {
                    previous = current;
                    continue;
                }
                if (IsConsecutive(current, previous.Value))
                {
                    consecutiveCount ++;
                    previous = current;
                    continue;
                }
                
                yield return new GroupDescriptor(consecutiveStart.Value, consecutiveCount);
                consecutiveStart = current;
                previous = current;
                consecutiveCount = 1;
            }
            if (consecutiveStart.HasValue)
            {
                yield return new GroupDescriptor(consecutiveStart.Value, consecutiveCount);
            }
        }
    }
}
