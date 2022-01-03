using Microsoft.Win32;
using System;
using System.Windows;

namespace RestTray.WindowsActions
{
    public class SystemEventDetections
    {
        private readonly HeartBeat _heartBeat;
        private readonly RestTimer _restTimer;
        private readonly Notification _notification;

        public SystemEventDetections(HeartBeat heartBeat, 
            RestTimer restTimer, 
            Notification notification)
        {
            _heartBeat = heartBeat;
            _restTimer = restTimer;
            _notification = notification;
        }
        public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _heartBeat.Stop();
                _restTimer.Start();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                //I returned to my desk
                _heartBeat.Start();
                _restTimer.Stop();
                _notification.ShowRestTimeNotification(_restTimer.GetElapsedTime());
                //var result = MessageBox.Show($"You rested for {_restTimer.GetElapsedTime().ToString("0.##")} min");                
            }
        }
    }
}
