using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace RestTray
{
    public class Notification
    {
        private readonly RestAction _restAction;

        public Notification(RestAction restAction)
        {
            _restAction = restAction;
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
                                Text = "Take a break!"
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
                _restAction.TakeRest();
            }
        }

        public void ShowRestTimeNotification(TimeSpan timeRested)
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
                                Text = $"You rested for: {timeRested.ToString(@"hh\:mm\:ss")}",                                
                            }
                        }
                    }
                },                

            };

            var doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());

            var promptNotification = new ToastNotification(doc);            

            ToastNotificationManagerCompat.CreateToastNotifier().Show(promptNotification);
        }
    }
}
