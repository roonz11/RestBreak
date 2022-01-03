using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RestTray.WindowsActions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;

namespace RestTray
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;
        
        private ServiceProvider serviceProvider;
        private HeartBeat _heartBeat;
        private SystemEventDetections _eventDetections;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();            
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<HeartBeat>();
            services.AddSingleton<RestTimer>();
            services.AddSingleton<Notification>();
            services.AddSingleton<RestAction>();
            services.AddSingleton<SystemEventDetections>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = RestTray.Resources.Icon;
            _notifyIcon.Visible = true;

            CreateContextMenu();

            _eventDetections = serviceProvider.GetService<SystemEventDetections>();
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(_eventDetections.SystemEvents_SessionSwitch);
            
            _heartBeat = serviceProvider.GetService<HeartBeat>();
            StartHeartBeat();
        }

        private void StartHeartBeat()
        {            
            _heartBeat.Start();
        }

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
            
        }

        private void ExitApplication()
        {
            _isExit = true;
            _heartBeat.Stop();
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide();
            }
        }
    }
}
