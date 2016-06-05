using System.Collections.Generic;

namespace Consecutive.Core.Partition
{
    public class GroupDescriptor
    {
        public GroupDescriptor(uint consecutiveStart, int consecutiveCount)
        {
            ConsecutiveStart = consecutiveStart;
            ConsecutiveCount = consecutiveCount;
        }

        public uint ConsecutiveStart { get; }
        public int ConsecutiveCount { get; }

        public IEnumerable<uint> GetSequence()
        {
            for (uint i = 0; i < ConsecutiveCount; i++)
            {
                yield return ConsecutiveStart + i;
            }
        }

        protected bool Equals(GroupDescriptor other)
        {
            return ConsecutiveStart == other.ConsecutiveStart && ConsecutiveCount == other.ConsecutiveCount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GroupDescriptor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) ConsecutiveStart*397) ^ ConsecutiveCount;
            }
        }
    }
}