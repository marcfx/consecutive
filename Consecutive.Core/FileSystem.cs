using System.Collections.Generic;
using System.IO;

namespace Consecutive.Core
{
    public class FileSystem : IFileSystem
    {
        public FileSystem()
        {
            FilePattern= "part{0}.binary";
        }

        public string FilePattern { get; set; }

        public Stream OpenFileForWriting(int chunkNo)
        {
            return new FileStream(string.Format(FilePattern, chunkNo), FileMode.OpenOrCreate);
        }

        public IList<BinaryReader> OpenAllChunks(int chunkCount)
        {
            var streams = new List<BinaryReader>(chunkCount);
            for (int i = 0; i < chunkCount; i++)
            {
                var fs = new FileStream(string.Format(FilePattern, i), FileMode.Open);
                streams.Add(new BinaryReader(fs));
            }
            return streams;
        }

        public void DeleteAllChunks(int chunkCount)
        {
            for (int i = 0; i < chunkCount; i++)
            {
                File.Delete(string.Format(FilePattern, i));
            }
        }

        public IList<uint> ReadFileIntoMemory(int chunkNo)
        {
            FileStream fs = File.OpenRead(string.Format(FilePattern, chunkNo));
            List<uint> result = new List<uint>();
            using (var reader = new BinaryReader(fs))
            {
                long pos = 0;
                long length =  reader.BaseStream.Length;
                while (pos < length)
                {
                    result.Add(reader.ReadUInt32());
                    pos += sizeof(uint);
                }
            }
            return result;
        }
    }
}
