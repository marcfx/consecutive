using System;
using System.IO;
using System.Text;
using ShellProgressBar;

namespace Consecutive.Console
{
    class SampleGenerator
    {
        private int progressChunk = 1000000;
        public void GenerateSampleFile(string fileName, int numberOfRecords)
        {
            Random rnd = new Random();
            using (var pbar = new ProgressBar(numberOfRecords/ progressChunk, "Generating sample file", ConsoleColor.Cyan))
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.ASCII, 524288))
            {
                for (var i = 0; i < numberOfRecords; i++)
                {
                    uint n = (uint)Math.Abs(rnd.Next(0, int.MaxValue)) * 2;
                    sw.Write($"{ n } ");
                    if (i% progressChunk == 0)
                    {
                        pbar.Tick("Currently processing " + i);
                    }
                }
            }
        }
    }
}
