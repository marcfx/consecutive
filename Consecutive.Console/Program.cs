using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;

namespace Consecutive.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.SampleFile != null)
                {
                    new SampleGenerator().GenerateSampleFile(options.SampleFile, options.SampleFileSize);
                }
                using (TextWriter outputWriter = GetOutputWriter(options))
                {
                    Bootstrap().ProcessFile(options.InputFile, outputWriter);
                }
            }
        }

        private static TextWriter GetOutputWriter(Options options)
        {
            return options.IsOutputFileSet ? File.CreateText(options.OutputFile) : System.Console.Out;
        }

        private static ConsecutiveApplication Bootstrap()
        {
            return new ConsecutiveApplication(new GroupConverter(), new ConsecutivePartitioner(), System.Console.Out);
        }
    }
}
