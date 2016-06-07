using System.IO;
using Consecutive.Core.ProgressBar;

namespace Consecutive.Core.ExternalMergeSort
{
    public class FileSplitter
    {
        private readonly IFileSystem _fileSystem;
        private readonly IProgress _progress;

        public FileSplitter(IFileSystem fileSystem, IProgress progress )
        {
            _fileSystem = fileSystem;
            _progress = progress;
            NumbersPerFile = 10000000;
        }

        private int _chunkNo = 0;

        public uint NumbersPerFile { get; set; }
        public void SplitUIntsIntoBinaryFiles(StreamReader reader)
        {
            BinaryWriter sw = OpenNewWriter();
            int counter = 0;
            _progress.Start("Generating chunks", 100);
            while (reader.Peek() >= 0)
            {
                counter++;
                sw.Write(reader.ReadUInt());
                if (reader.Peek() < 0)
                {
                    sw.Close();
                    break;
                }
                if (counter % NumbersPerFile == 0)
                {
                    sw.Close();
                    ++_chunkNo;
                    sw = OpenNewWriter();
                    _progress.Tick("Writing chunk " + _chunkNo);
                }
            }
            _progress.Dispose();
        }

        public int NumberOfChunks => _chunkNo+1;

        private BinaryWriter OpenNewWriter()
        {
            return new BinaryWriter(_fileSystem.OpenFileForWriting(_chunkNo));
        }
    }
}
