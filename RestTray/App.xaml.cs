using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RestTray.Data;
using RestTray.Options;
using RestTray.Repositories;
using RestTray.Services;
using RestTray.Timers;
using RestTray.WindowsActions;
using System;
using System.ComponentModel;
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
        public IConfiguration Configuration { get; private set; }

        private IHeartBeat _heartBeat;
        private ActiveTimer _activeTimer;
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
            services.AddDbContext<RestBreakContext>();

            services.AddLogging();

            services.AddSingleton<IHeartBeat, HeartBeat>();
            services.AddSingleton<INotification, Notification>();
            services.AddSingleton<IRestAction, RestAction>();
            services.AddSingleton<ActiveTimer>();
            services.AddSingleton<RestTimer>();
            services.AddSingleton<SystemEventDetections>();
            services.AddScoped<ISessionRepository, SessionRepository>();
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
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(async (s, e) => await _eventDetections.SystemEvents_SessionSwitch(s, e));

            _heartBeat = ServiceProvider.GetService<IHeartBeat>();
            _activeTimer = ServiceProvider.GetService<ActiveTimer>();
            StartHeartBeat();
        }

        private void StartHeartBeat()
        {
            //_heartBeat.Start();
            //_activeTimer.Start();
        }

        //private void Cleanup()
        //{
        //    var seessionRepo = ServiceProvider.GetService<ISessionRepository>();
        //    seessionRepo.RemoveAll();
        //}

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Stats").Click += (s, e) => ShowStats();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();

        }

        private void ShowStats()
        {
            var statsWindow = new Stats(ServiceProvider.GetService<ISessionRepository>());
            statsWindow.Show();
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
