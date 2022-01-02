using RestBreakService;
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
        private HeartBeat _heartBeat = new HeartBeat();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            //_notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            _notifyIcon.Icon = RestTray.Resources.Icon;
            _notifyIcon.Visible = true;

            CreateContextMenu();
            StartRestService();
        }

        private void StartRestService()
        {            
            _heartBeat.Start();
        }

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            //_notifyIcon.ContextMenuStrip.Items.Add("Rest Bro").Click += (s, e) => ShowMainWindow();
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

        public Icon ByteToIcon(byte[] bytes)
        {
            using(MemoryStream ms = new MemoryStream(bytes))
            {
                return new Icon(ms);
            }
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
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
