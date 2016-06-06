using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace Consecutive.Console
{
    class Options
    {
        [Option('i', "input", Required = true,
          HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false,
          HelpText = "Output file.")]
        public string OutputFile { get; set; }

        [Option('s', "sample", Required = false,
  HelpText = "Generate sample file.")]
        public string SampleFile { get; set; }

        [Option('x', "sampleSize", Required = false,
  HelpText = "Sample file size.")]
        public int SampleFileSize { get; set; }
        public bool IsOutputFileSet
        {
            get { return !string.IsNullOrEmpty(OutputFile); }
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
