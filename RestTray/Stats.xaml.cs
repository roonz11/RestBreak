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
            _sessionsStackedBarChart = sessionsStackedBarChart;
            grid1.Children.Add(_sessionsStackedBarChart);
        }
    }
}
