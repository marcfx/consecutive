using System;

namespace Consecutive.Core.ProgressBar
{
    class Progress : IProgress
    {
        private ShellProgressBar.ProgressBar _progressBar;

        public Progress()
        {
        }

        public void Start(string message, int ticks)
        {
            _progressBar = new ShellProgressBar.ProgressBar(ticks, message, ConsoleColor.Cyan);
        }

        public void Tick(string message)
        {
            _progressBar.Tick(message);
        }

        public void Dispose()
        {
            _progressBar.Dispose();
        }
    }
}