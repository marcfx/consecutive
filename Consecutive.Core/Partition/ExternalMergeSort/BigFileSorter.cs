using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consecutive.Core.Partition.ExternalMergeSort
{
    public class BigFileSorter
    {
        private readonly FileSplitter _fileSplitter;
        private readonly IFileSystem _fileSystem;
        private readonly BinaryStreamSorter _binaryStreamSorter;
        private readonly FilePartMerger _filePartMerger;

        public BigFileSorter(FileSplitter fileSplitter, IFileSystem fileSystem, BinaryStreamSorter binaryStreamSorter, FilePartMerger filePartMerger)
        {
            _fileSplitter = fileSplitter;
            _fileSystem = fileSystem;
            _binaryStreamSorter = binaryStreamSorter;
            _filePartMerger = filePartMerger;
        }

        public void Sort(string fileName)
        {
            _fileSplitter.SplitUintsIntoBinaryFiles(File.OpenText(fileName));
            for (int i = 0; i < _fileSplitter.NumberOfChunks; i++)
            {
                using (Stream stream = _fileSystem.OpenFileForWriting(i))
                {
                    _binaryStreamSorter.SortBinaryFileInMemory(stream);
                }
            }
            _filePartMerger.MergeSortParts(_fileSplitter.NumberOfChunks, fileName);
        }
    }
}
