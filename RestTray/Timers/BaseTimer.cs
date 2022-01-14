using System;

namespace RestTray.Timers
{
    public abstract class BaseTimer
    {
        public DateTime StartTime;
        public DateTime EndTime;

        public void Start()
        {
            StartTime = DateTime.Now;
        }
        public void Stop()
        {
            EndTime = DateTime.Now;
        }

        public TimeSpan GetElapsedTime()
        {
            return EndTime - StartTime;
        }
    }
}
