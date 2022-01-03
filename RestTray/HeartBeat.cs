using System.Timers;

namespace RestTray
{
    public class HeartBeat
    {
        private readonly Timer _timer;
        private readonly Notification _notification;
#if DEBUG
        private const int DURATION = 10000;
#else
        private const int DURATION = 20 * 60000;
#endif

        public HeartBeat(Notification notification)
        {
            _notification = notification;
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

        public void Restart()
        {
            _timer.Stop();
            _timer.Start();
        }
    }

    
}
