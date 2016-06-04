using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consecutive.Console
{
    static class PartitionExtension
    {
        public static IEnumerable<Tuple<int, int>> Partition(this uint[] values)
        {
            if (values == null || values.Length == 0)
            {
                yield break;
            }

            int startIndex = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > values[i - 1] + 1)
                {
                    yield return Tuple.Create(startIndex, i - startIndex);
                    startIndex = i;
                }
            }

            yield return Tuple.Create(startIndex, values.Length - startIndex);
        }
    }
}
