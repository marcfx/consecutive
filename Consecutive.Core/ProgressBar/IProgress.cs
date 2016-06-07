using System.Security.Cryptography.X509Certificates;

namespace Consecutive.Core.ProgressBar
{
    public interface IProgress
    {
        void Start(string message, int ticks);
        void Tick(string message);

        void Dispose();
    }
}