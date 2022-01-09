using System;

namespace RestTray.Services
{
    public interface INotification
    {
        void ShowNotification();
        void ShowRestTimeNotification(TimeSpan timeRested);
    }
}