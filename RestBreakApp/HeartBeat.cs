using System;
using System.IO;
using System.Timers;
using Topshelf;

namespace RestBreakService
{
    public class HeartBeat
    {
        private readonly Timer _timer;
        private readonly Notification _notification;
#if DEBUG
        private const int DURATION = 3000;
#else
        private const int DURATION = 20 * 60000;
#endif

        public HeartBeat()
        {
            _notification = new Notification();
            _timer = new Timer(DURATION) { AutoReset = true };
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

        //public void PowerEvent(HostControl host, PowerEventArguments arg)
        //{
        //    File.WriteAllLines(@"C:\Users\Aruna\Documents\deleteme\powerevents.txt", new string[] {
        //                    host.ToString(),
        //                    arg.EventCode.ToString(),
        //                    _timer.Interval.ToString()
        //                });
        //}
    }
}
