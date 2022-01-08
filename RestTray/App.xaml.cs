using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RestTray.Data;
using RestTray.Options;
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
        
        public IServiceProvider ServiceProvider { get; private set; }
        public  IConfiguration Configuration { get; private set; }
        
        private HeartBeat _heartBeat;
        private SystemEventDetections _eventDetections;

        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

#if DEBUG
                builder.AddJsonFile("appSettings.Development.json", optional: true);
#else
                builder.AddJsonFile("appSettings.Production.json", optional: true);

#endif
            Configuration = builder.Build();

            
            
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();            
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestBreakContext>(options => 
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<HeartBeat>();
            services.AddSingleton<RestTimer>();
            services.AddSingleton<Notification>();
            services.AddSingleton<RestAction>();
            services.AddSingleton<SystemEventDetections>();            
            services.Configure<BreakInterval>(Configuration.GetSection(nameof(BreakInterval)));
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

            _eventDetections = ServiceProvider.GetService<SystemEventDetections>();
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(_eventDetections.SystemEvents_SessionSwitch);
            
            _heartBeat = ServiceProvider.GetService<HeartBeat>();
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
