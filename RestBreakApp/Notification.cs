using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.IO;
using System.Reflection;

namespace RestBreakService
{
    public class Notification
    {
        private readonly string projectName;
        private int Id;
        public Notification()
        {
            projectName = Assembly.GetCallingAssembly().GetName().Name;
            Id = 0;
        }
        public void ShowNotification()
        {
            new ToastContentBuilder()
                .AddArgument("action", "showNotification")
                .AddArgument("notificationId", Id)
                .SetToastScenario(ToastScenario.Reminder)                
                .AddText("Time to take a break")
                .AddText("Take a break dammit!")                
                .Show();
        }
    }
}
