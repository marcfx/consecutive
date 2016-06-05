using System.IO;
using System.Text;

namespace Consecutive.Core.BigFileSorting
{
    public class FileSplitter
    {
        private readonly IFileSystem _fileSystem;

        public FileSplitter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            NumbersPerFile = 10000000;
        }

        private int _chunkNo = 0;

        public uint NumbersPerFile { get; set; }
        public void SplitUintsIntoBinaryFiles(StreamReader reader)
        {
            BinaryWriter sw = OpenNewWriter();
            int counter = 0;
            while (reader.Peek() >= 0)
            {
                counter++;
                sw.Write(ReadUint(reader));
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
                }
            }
        }

        public int NumberOfChunks => _chunkNo;

        private BinaryWriter OpenNewWriter()
        {
            return new BinaryWriter(_fileSystem.OpenFileForWriting(_chunkNo));
        }

        private uint ReadUint(TextReader sr)
        {
            StringBuilder sb = new StringBuilder();
            while (sr.Peek() >= 0)
            {
                char current = (char)sr.Read();
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
