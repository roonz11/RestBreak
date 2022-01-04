using Microsoft.Extensions.Options;
using RestTray.Options;
using System.Timers;

namespace RestTray
{
    public class HeartBeat
    {
        private readonly Timer _timer;
        private readonly Notification _notification;
        private readonly BreakInterval _restOptions;

        public HeartBeat(Notification notification, IOptions<BreakInterval> restOptions)
        {
            _notification = notification;
            _restOptions = restOptions.Value;
            _timer = new Timer(_restOptions.DurationMilliSeconds) { AutoReset = true };            
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
