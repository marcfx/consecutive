using System;
using System.Collections.Generic;
using System.IO;

namespace Consecutive.Core.ExternalMergeSort
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

        public IEnumerable<uint> Sort(string fileName)
        {
            _fileSplitter.SplitUIntsIntoBinaryFiles(File.OpenText(fileName));

            using (var pbar = new ShellProgressBar.ProgressBar(_fileSplitter.NumberOfChunks, "Generating chunks", ConsoleColor.Cyan))
            for (int i = 0; i < _fileSplitter.NumberOfChunks; i++)
            {
                pbar.Tick("Sorting chunk " + i);
                using (Stream stream = _fileSystem.OpenFileForWriting(i))
                {
                    _binaryStreamSorter.SortBinaryFileInMemory(stream);
                }
            }
            return _filePartMerger.MergeSortParts(_fileSplitter.NumberOfChunks);
        }
    }
}
