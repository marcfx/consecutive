using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consecutive.Core;

namespace Consecutive.Console
{
    class Program
    {
        static void Main2(string[] args)
        {
            StringBuilder output= new StringBuilder();
            using (StreamWriter sw = new StreamWriter("Test1.txt", false, Encoding.ASCII, 524288))
            {
                for (uint i = 0; i < uint.MaxValue; i++)
                {
                    sw.Write($"{i} ");
                }
            }
        }

        static void Main(string[] args)
        {
            List<uint> big = new List<uint>();
            for (uint i = 0; i < uint.MaxValue; i++)
            {
                big.Add(i);
            }
        }

        //public static void Main(string[] args)
        //{
        //    var values = new uint[] { 1, 2, 3, 18, 19, 24,25 };
        //    foreach (var tuple in values.Partition())
        //    {
        //        System.Console.WriteLine(values[tuple.Item1] + "-" + tuple.Item2);
        //        //Enumerable.Range(values[tuple.Item1], values[tuple.Item2]).Select


        //    }
        //}
    }
}
