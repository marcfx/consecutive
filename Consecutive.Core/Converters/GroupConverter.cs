using System;
using System.Collections.Generic;
using System.Linq;
using Consecutive.Core.Partition;

namespace Consecutive.Core.Converters
{
    public class GroupConverter : IGroupConverter
    {
        public string Convert(IEnumerable<uint> numbers)
        {
            string numbersString = string.Join(",", numbers);
            return $"{{{numbersString}}}";
        }

        public string Convert(IEnumerable<GroupDescriptor> groupDescriptors)
        {
            IEnumerable<string> groupStrings = groupDescriptors.Select(g => g.GetSequence()).Select(Convert);
            return string.Join(Environment.NewLine, groupStrings);
        }


        public IEnumerable<uint> Parse(string numbersInString)
        {
            return numbersInString.Replace("{", "").Replace("}", "")
                .Split(new [] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(uint.Parse);
        }
    }
}
