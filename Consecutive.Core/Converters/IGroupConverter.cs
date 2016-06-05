using System.Collections.Generic;
using Consecutive.Core.Partition;

namespace Consecutive.Core.Converters
{
    public interface IGroupConverter
    {
        string Convert(IEnumerable<uint> numbers);
        string Convert(IEnumerable<GroupDescriptor> groupDescriptors);
        IEnumerable<uint> Parse(string numbersInString);
    }
}