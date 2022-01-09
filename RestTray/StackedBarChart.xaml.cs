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
        public Func<ChartPoint, string> LabelPointFormatter { get; set; }
        public Func<double, string> LabelFormatter { get; set; }
        public SessionsStackedBarChart(IEnumerable<Session> sessions)
        {
            InitializeComponent();
            _sessions = sessions;
            CreateChart();
        }

        private void CreateChart()
        {
            LabelPointFormatter = value => TimeSpan.FromSeconds(value.Y).ToString(@"hh\:mm\:ss");

            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Name = "Active",
                    Title = "Active",
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = LabelPointFormatter,
                    FontFamily = new FontFamily("Helvetica Light"),
                    Values = new ChartValues<double>(_sessions.Select(x => x.RestTime)),
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill = Brushes.DarkRed
                },
                new StackedColumnSeries
                {
                    Name = "Resting",
                    Title = "Resting",
                    LabelsPosition = BarLabelPosition.Top,
                    LabelPoint = LabelPointFormatter,
                    FontFamily = new FontFamily("Helvetica Light"),
                    Values = new ChartValues<double>(_sessions.Select(x => x.ActiveTime)),
                    StackMode = StackMode.Values,
                    DataLabels = true,
                    Fill = Brushes.LightSkyBlue
                }
            };

            Labels = _sessions.Select(x => x.Date.ToString("HH:mm")).ToArray();


            LabelFormatter = value => TimeSpan.FromSeconds(value).ToString(@"mm");

            DataContext = this;
        }
    }
}
