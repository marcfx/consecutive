using System;
using System.IO;
using System.Text;
using ShellProgressBar;

namespace Consecutive.Core.BigFileUniqueNumbers
{
    public class BitmaskUintFinder
    {
        public BitArrayUint FindAllUints(StreamReader stream)
        {
            var uintNumbers = new BitArrayUint();
            using (var pbar = new ProgressBar(3000, "Processing input file", ConsoleColor.Cyan))
            {
                while (stream.Peek() >= 0)
                {
                    uint current = ReadUint(stream);
                    uintNumbers.Set(current, true);
                    if (current%1000000 < 100)
                    {
                        pbar.Tick("Reading input file ");
                    }
                }
            }
            
            return uintNumbers;
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
