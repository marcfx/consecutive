using System;
using System.IO;

namespace Consecutive.Core.ExternalMergeSort
{
    public class BinaryStreamSorter
    {
        public void SortBinaryFileInMemory(Stream stream)
        {
            uint[] records = new uint[stream.Length / sizeof(uint)];
            ReadDataFromStream(stream, records);
            Array.Sort(records);
            stream.Position = 0;
            WriteDataToStream(stream, records);
        }

        private static void WriteDataToStream(Stream stream, uint[] records)
        {
            var bw = new BinaryWriter(stream);
            foreach (uint record in records)
            {
                bw.Write(record);
            }
        }

        private static void ReadDataFromStream(Stream stream, uint[] records)
        {
            var br = new BinaryReader(stream);
            for (int i = 0; i < records.Length; i++)
            {
                records[i] = br.ReadUInt32();
            }
        }
    }
}
