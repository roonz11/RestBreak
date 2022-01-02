using Microsoft.Toolkit.Uwp.Notifications;
using RestTray.WindowsActions;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace RestTray
{
    public class Notification
    {
        public void ShowNotification()
        {
            ToastContent toastContent = new ToastContent()
            {
                Launch = "body tapped",
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Take a break fool"
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Fine", "true"),
                        new ToastButton("Go Away", "false")
                    }

                },

            };

            var doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());

            var promptNotification = new ToastNotification(doc);
            promptNotification.Activated += PromptNotificationOnActivated;

            ToastNotificationManagerCompat.CreateToastNotifier().Show(promptNotification);
        }

        private void PromptNotificationOnActivated(ToastNotification sender, object args)
        {
            ToastActivatedEventArgs strArgs = args as ToastActivatedEventArgs;

            if (strArgs.Arguments == "true")
            {

                DllComands.SendMessage(Monitor.HWND_BROADCAST, Monitor.WM_SYSCOMMAND, Monitor.SC_MONITORPOWER, (int)Monitor.MonitorState.OFF);
                //DllComands.LockWorkStation();
                //DllComands.SetSuspendState(false, true, true);
            }
        }
    }
}
