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
        static void Main(string[] args)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter("Test3.txt", false, Encoding.ASCII, 524288))
            {
                for (uint i = 0; i < 1000*1000*1000*2.9; i++)
                {
                    uint n = (uint)Math.Abs(rnd.Next(0,int.MaxValue))*2;
                    sw.Write($"{ n } ");
                }
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
