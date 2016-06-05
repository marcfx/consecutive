using System.Collections.Generic;
using System.IO;

namespace Consecutive.Core
{
    public interface IFileSystem
    {
        Stream OpenFileForWriting(int chunkNo);

        IList<uint> ReadFileIntoMemory(int chunkNo);
        string FilePattern { get; set; }
        IList<BinaryReader> OpenAllChunks(int chunkCount);

        void DeleteAllChunks(int chuckCount);
    }
}