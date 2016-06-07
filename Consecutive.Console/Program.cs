using System;
using System.IO;
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
                GenerateSampleFile(options);
                System.Console.WriteLine($"Started at {DateTime.Now.ToLongTimeString()}.");
                Process(options);
                System.Console.WriteLine($"Finished at {DateTime.Now.ToLongTimeString()}.");
            }
        }

        private static void Process(Options options)
        {
            System.Console.WriteLine($"Processing using {options.Algorithm} algorithm.");
            switch (options.Algorithm)
            {
                case Algorithm.BitMask:
                {
                    using (TextWriter outputWriter = GetOutputWriter(options))
                    {
                        Bootstrap().ProcessFile(options.InputFile, outputWriter);
                    }
                    break;
                }
                case Algorithm.InMemorySimple:
                {
                    string result = Bootstrap().Process(File.ReadAllText(options.InputFile));
                    File.WriteAllText(options.OutputFile, result);
                    break;
                }
                case Algorithm.ExternalMergeSort:
                {
                    using (TextWriter outputWriter = GetOutputWriter(options))
                    {
                        Bootstrap().ProcessMergeSort(options.InputFile, outputWriter);
                    }
                    break;
                }
            }
        }

        private static void GenerateSampleFile(Options options)
        {
            if (options.SampleFile != null)
            {
                new SampleGenerator().GenerateSampleFile(options.SampleFile, options.SampleFileSize);
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
