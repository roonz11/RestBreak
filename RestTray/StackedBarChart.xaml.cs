using LiveCharts;
using LiveCharts.Wpf;
using RestTray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace RestTray
{
    /// <summary>
    /// Interaction logic for StackedBarChart.xaml
    /// </summary>
    public partial class SessionsStackedBarChart : UserControl
    {
        private readonly IEnumerable<Session> _sessions;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> LabelFormatter { get; set; }
        public SessionsStackedBarChart(IEnumerable<Session> sessions)
        {
            InitializeComponent();
            _sessions = sessions;
            CreateChart();
        }

        private void CreateChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Name = "Active",
                    LabelsPosition = BarLabelPosition.Top,
                    FontFamily = new FontFamily("Helvetica Light"),
                    Values = new ChartValues<double>(_sessions.Select(x => x.RestTime)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill = Brushes.IndianRed
                },
                new StackedColumnSeries
                {
                    Name = "Resting",
                    LabelsPosition = BarLabelPosition.Top,
                    FontFamily = new FontFamily("Helvetica Light"),
                    Values = new ChartValues<double>(_sessions.Select(x => x.ActiveTime)),
                    StackMode = StackMode.Values,
                    DataLabels = true,
                    Fill = Brushes.MidnightBlue
                }
            };

            Labels = _sessions.Select(x => x.Date.ToString("d/M HH:mm")).ToArray();

            Formatter = value => TimeSpan.FromSeconds(value).ToString(@"hh\:mm\:ss");
            LabelFormatter = value => TimeSpan.FromSeconds(value).ToString(@"mm");

            DataContext = this;
        }
    }
}
