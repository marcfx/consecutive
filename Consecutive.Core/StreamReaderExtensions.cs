using System.IO;
using System.Text;

namespace Consecutive.Core
{
    public static class StreamReaderExtensions
    {
        public static uint ReadUInt(this StreamReader sr)
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
