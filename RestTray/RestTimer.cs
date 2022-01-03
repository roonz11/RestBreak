
using System;
using System.Diagnostics;

namespace RestTray
{
    public class RestTimer
    {
        private readonly Stopwatch _timer;
        public RestTimer()
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
