using System;
using System.Diagnostics;

namespace Assets.Core.Utils.Monitoring
{
    public sealed class TimedProfiler : IDisposable
    {
        private string message { get; }
        private Stopwatch timer { get; }

        public TimedProfiler(string message)
        {
            this.message = message;

            timer = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            timer.Stop();

            var time = timer.Elapsed;
            var ms = timer.ElapsedMilliseconds;

            Log.Warn($"{message} - Elapsed: {time} ({ms}ms)");
        }
    }
}
