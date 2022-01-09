using Microsoft.Win32;
using RestTray.Models;
using RestTray.Repositories;
using RestTray.Services;
using RestTray.Timers;
using System;
using System.Threading.Tasks;

namespace RestTray.WindowsActions
{
    public class SystemEventDetections
    {
        private readonly IHeartBeat _heartBeat;
        private readonly RestTimer _restTimer;
        private readonly ActiveTimer _activeTimer;
        private readonly INotification _notification;
        private readonly ISessionRepository _sessionRepository;

        public SystemEventDetections(IHeartBeat heartBeat,
            RestTimer restTimer,
            ActiveTimer activeTimer,
            INotification notification,
            ISessionRepository sessionRepository)
        {
            _heartBeat = heartBeat;
            _restTimer = restTimer;
            _activeTimer = activeTimer;
            _notification = notification;
            _sessionRepository = sessionRepository;
        }
        public async Task SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _heartBeat.Stop();
                _activeTimer.Stop();
                _restTimer.Start();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                var timeRested = _restTimer.GetElapsedTime();
                var session = new Session
                {
                    ActiveTime = _activeTimer.GetElapsedTime().TotalSeconds,
                    RestTime = timeRested.TotalSeconds,
                    Date = DateTime.Now
                };
                var result = await _sessionRepository.AddSessionAsync(session);
                _heartBeat.Start();
                _restTimer.Stop();
                _activeTimer.Start();
                _notification.ShowRestTimeNotification(timeRested);
            }
        }
    }
}
