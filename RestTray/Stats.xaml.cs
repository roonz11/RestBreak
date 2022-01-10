using System.ComponentModel;
using System.Windows;

namespace RestTray
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        private readonly SessionsStackedBarChart _sessionsStackedBarChart;

        public Stats(SessionsStackedBarChart sessionsStackedBarChart)
        {
            InitializeComponent();
            Closing += StatsClosing;
            _sessionsStackedBarChart = sessionsStackedBarChart;
            grid1.Children.Add(_sessionsStackedBarChart);
        }

        private void StatsClosing(object sender, CancelEventArgs e)
        {
            grid1.Children.RemoveAt(0);
        }
    }
}
