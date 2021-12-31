using System;
using System.IO;
using System.Timers;

namespace RestBreakService
{
    public class HeartBeat
    {
        private readonly Timer _timer;
        private readonly Notification _notification;

        public HeartBeat()
        {
            _notification = new Notification();
            _timer = new Timer(3000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _notification.ShowNotification();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
