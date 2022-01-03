
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

        public decimal GetElapsedTime()
        {
            //return _timer.ElapsedMilliseconds;            
#if DEBUG
            return _timer.ElapsedMilliseconds / 1000;
#else
            return _timer.ElapsedMilliseconds / 60000;            
#endif
        }
    }
}
