using System;
using System.IO;
using System.Text;
using Consecutive.Core;
using Consecutive.Core.Converters;
using Consecutive.Core.Partition;
using NUnit.Framework;

namespace Consecutive.Tests
{
    [TestFixture]
    public class ConsecutivePartitionerIntegrationTests
    {
        [TestCase("24,2,5,22,8,4,3,23,22", "{2,3,4,5},{8},{22,23,24}")]
        public void ProcessTest(string input, string expected)
        {
            var target = new ConsecutiveApplication(new GroupConverter(), new PartitionInMemory());
            string result = target.Process(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("Test1.txt")]
        public void ProcessFileTest(string file)
        {
            //var target = new ConsecutiveApplication(new GroupConverter(), new PartitionInMemory());
            //string result = target.Process(input);
            //Assert.AreEqual(expected, result);

            using (
                StreamReader sr =
                    new StreamReader(@"c:\Users\Marek\Source\Repos\consecutive\Consecutive.Console\bin\Debug\Test1.txt")
                )
            {
                int chunkNo = 1;
                BinaryWriter sw = MakeStreamWriter(chunkNo);
                int counter = 0;
                while (sr.Peek() >= 0)
                {
                    counter++;
                    sw.Write(ReadUint(sr));
                    if (counter%100000000 == 0)
                    {
                        sw.Close();
                        sw = MakeStreamWriter(++chunkNo); ;
                    }
                }
            }
        }

        private static BinaryWriter MakeStreamWriter(int chunkNo)
        {
            return new BinaryWriter(new FileStream($"c:\\Users\\Marek\\Source\\Repos\\consecutive\\Consecutive.Console\\bin\\Debug\\chunk{chunkNo}.binary", FileMode.Create));
        }

        private uint ReadUint(StreamReader sr)
        {
            StringBuilder sb = new StringBuilder();
            while (sr.Peek() >= 0)
            {
                char current = (char) sr.Read();
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
