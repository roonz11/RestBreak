using Microsoft.Toolkit.Uwp.Notifications;

namespace RestBreakService
{
    public class Notification
    {
        public void ShowNotification()
        {
            new ToastContentBuilder()
                .AddArgument("action", "showNotification")
                .AddArgument("notificationId", 1)
                .AddText("Time to take a break")
                .AddText("Take a break dammit!")
                .Show();
        }
    }
}
