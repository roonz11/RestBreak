using System;
using System.Diagnostics;

namespace RestTray.Timers
{
    public abstract class BaseTimer
    {
        private readonly Stopwatch _timer;
        public BaseTimer()
        {
            _timer = new Stopwatch();
        }

        public void Start()
        {
            _timer.Restart();
        }
        public void Stop()
        {
            _timer.Stop();
        }

        public TimeSpan GetElapsedTime()
        {
            return TimeSpan.FromMilliseconds(_timer.ElapsedMilliseconds);
        }
    }
}
