using System.Collections.Generic;
using System.IO;

namespace Consecutive.Core.Partition.ExternalMergeSort
{
    public class FilePartMerger
    {
        private readonly IFileSystem _fileSystem;

        const int ExpectedMemoryUsage = 500000000; 
        const double QueueOverhead = 7.5; 

        public FilePartMerger(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void MergeSortParts(int chunkCount, string outputFile)
        {
            int bufferSize = ExpectedMemoryUsage / chunkCount; 
            int bufferNoOfRecords = (int)(bufferSize / sizeof(uint) / QueueOverhead); 

            IList<BinaryReader> readers = _fileSystem.OpenAllChunks(chunkCount);

            Queue<uint>[] queues = MakeQueues(chunkCount, bufferNoOfRecords);

            LoadDataToQueues(queues, readers, bufferNoOfRecords);

            MergeSort(outputFile, queues, readers, bufferNoOfRecords);

            _fileSystem.DeleteAllChunks(chunkCount);

        }

        private static void MergeSort(string outputFile, Queue<uint>[] queues, IList<BinaryReader> readers, int bufferLen)
        {
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                while (true)
                {
                    var lowestIndex = -1;
                    var lowestValue = FindLowestValue(queues, ref lowestIndex);

                    if (WasNothingFoundInAnyQueue(lowestIndex))
                    {
                        break;
                    }

                    sw.WriteLine(lowestValue);
                    queues[lowestIndex].Dequeue();

                    LoadDataToQueueWhenNeeded(ref queues[lowestIndex], readers[lowestIndex], bufferLen);
                }
            }
        }

        private static bool WasNothingFoundInAnyQueue(int lowestIndex)
        {
            return lowestIndex == -1;
        }

        private static uint FindLowestValue(Queue<uint>[] queues, ref int lowestIndex)
        {
            uint lowestValue = uint.MaxValue;
            for (int j = 0; j < queues.Length; j++)
            {
                if (queues[j].Count == 0) continue;
                if (lowestIndex < 0 || queues[j].Peek() < lowestValue)
                {
                    lowestIndex = j;
                    lowestValue = queues[j].Peek();
                }
            }
            return lowestValue;
        }

        private static void LoadDataToQueues(Queue<uint>[] queues, IList<BinaryReader> readers, int bufferlen)
        {
            for (int i = 0; i < queues.Length; i++)
            {
                LoadQueue(queues[i], readers[i], bufferlen);
            }
        }

        private static void LoadDataToQueueWhenNeeded(ref Queue<uint> queue, BinaryReader reader, int bufferlen)
        {
            if (queue.Count != 0) return;
            LoadQueue(queue, reader, bufferlen);
        }

        private static Queue<uint>[] MakeQueues(int chunkCount, int bufferLen)
        {
            var queues =  new Queue<uint>[chunkCount];
            for (int i = 0; i < chunkCount; i++)
                queues[i] = new Queue<uint>(bufferLen);
            return queues;
        }

        static void LoadQueue(Queue<uint> queue, BinaryReader reader, int records)
        {
            if (reader.BaseStream==null)
            {
                return;
            }
            for (int i = 0; i < records; i++)
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                {
                    reader.Close();
                    return;
                }
                queue.Enqueue(reader.ReadUInt32());
            }
        }
    }
}
