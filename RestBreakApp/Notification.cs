using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace RestBreakService
{
    public class Notification
    {
        private int id;
        public Notification()
        {            
            id = 0;
        }
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
                    Buttons = { new ToastButton("Fine", "true") }

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

            if(strArgs.Arguments == "true")
            {
                WindowsActions.LockWorkStation();
            }
        }
    }
}
