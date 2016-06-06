using System.Collections;
using System.IO;
using System.Text;

namespace Consecutive.Core.BigFileUniqueNumbers
{
    public class BitmaskUitFinder
    {
        private int UintCountOfNumbers = 429496729;//6;
        public BitArray FindAllUints(StreamReader stream)
        {
            BitArray uintNumbers = new BitArray(UintCountOfNumbers);
            while (stream.Peek() >= 0)
            {
                uint current = ReadUint(stream);
                int currentInt = StoreUintInInt(current);
                uintNumbers.Set(currentInt, true);
            }
            return uintNumbers;
        }

        private int StoreUintInInt(uint value)
        {
            return (int)(uint.MaxValue - int.MaxValue - 1);
        }

        private uint ReadUint(StreamReader sr)
        {
            StringBuilder sb = new StringBuilder();
            while (sr.Peek() >= 0)
            {
                char current = (char)sr.Read();
                if (current == ' ')
                {
                    break;
                }
                sb.Append(current);
            }
            return uint.Parse(sb.ToString());
        }
    }
}
