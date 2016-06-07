using System.IO;
using Consecutive.Core.ProgressBar;

namespace Consecutive.Core.BitmaskSort
{
    public class BitmaskUIntFinder
    {
        private readonly IProgress progress;

        public BitmaskUIntFinder(IProgress progress)
        {
            this.progress = progress;
        }

        public BitArrayUInt FindAllUInts(StreamReader stream)
        {
            var uIntNumbers = new BitArrayUInt();
            progress.Start("Processing input file", 1000);
            int i = 0;
            while (stream.Peek() >= 0)
            {
                uint current = stream.ReadUInt();
                uIntNumbers.Set(current, true);
                if (i++%100000==0)
                {
                progress.Tick("Reading number " + i);
                }
            }
            progress.Dispose();
            return uIntNumbers;
        }
    }
}
