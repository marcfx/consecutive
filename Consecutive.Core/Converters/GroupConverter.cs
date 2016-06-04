using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consecutive.Core.Converters
{
    public class GroupConverter : IGroupConverter
    {
        const string ElementSeparator = ",";
        public string Convert(IEnumerable<uint> numbers)
        {
            string numbersString = string.Join(ElementSeparator, numbers);
            return $"{{{numbersString}}}";
        }

        public string Convert(IEnumerable<GroupDescriptor> groupDescriptors)
        {
            IEnumerable<string> groupStrings = groupDescriptors.Select(g => g.GetSequence()).Select(Convert);
            return string.Join(ElementSeparator, groupStrings);
        }

        public string Convert(IEnumerable<IEnumerable<uint>> numbers)
        {
            return string.Join(ElementSeparator, numbers.Select(Convert));
        }

        public IEnumerable<uint> Parse(string numbersInString)
        {
            return numbersInString.Replace("{", "").Replace("}", "")
                .Split(new [] { ElementSeparator }, StringSplitOptions.RemoveEmptyEntries)
                .Select(uint.Parse);
        }
    }
}
