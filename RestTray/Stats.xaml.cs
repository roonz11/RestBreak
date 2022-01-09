using RestTray.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace RestTray
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        private readonly ISessionRepository _sessionRepository;

        public Stats(ISessionRepository sessionRepository)
        {
            InitializeComponent();
            _sessionRepository = sessionRepository;
        }

        protected override void OnActivated(EventArgs e)
        {
            var sesssionTask = _sessionRepository.GetSessionsAsync();
            Task.WhenAll(sesssionTask);
            var sessions = sesssionTask.Result;
        }
    }
}
