using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Extensions.Options;
using RestTray.Models;
using RestTray.Options;
using RestTray.Repositories;
using RestTray.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace RestTray
{
    /// <summary>
    /// Interaction logic for StackedBarChart.xaml
    /// </summary>
    public partial class SessionsStackedBarChart : UserControl
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly BreakInterval _restOptions;
        private readonly ActiveTimer _activeTimer;
        private IList<Session> _sessions;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<ChartPoint, string> LabelPointFormatter { get; set; }
        public Func<double, string> LabelFormatter { get; set; }

        const string TIMEFORMAT = @"hh\:mm\:ss";

        public string AvgActive { get; set; }
        public string AvgResting { get; set; }

        public int daysFilter = -1;
        public string NextBreak { get; set; }
        public SessionsStackedBarChart(ISessionRepository sessionRepository,
            IOptions<BreakInterval> restOptions,
            ActiveTimer activeTimer)
        {
            _sessionRepository = sessionRepository;
            _restOptions = restOptions.Value;
            _activeTimer = activeTimer;
            InitializeComponent();
            RefreshData();
        }

        private void CalculateNextBreak()
        {
            NextBreak = TimeSpan.FromMilliseconds(_restOptions.DurationMilliSeconds - _activeTimer.GetElapsedTime().TotalMilliseconds)
                .ToString(TIMEFORMAT);
        }

        private void CreateChart()
        {
            LabelPointFormatter = value => TimeSpan.FromSeconds(value.Y).ToString(TIMEFORMAT);

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
                    //DataLabels = true,
                    Fill = Brushes.White,
                    Stroke = Brushes.White,
                    StrokeThickness = 0,
                    MaxColumnWidth = 5,
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
                    //DataLabels = true,
                    Fill = Brushes.LightSkyBlue,
                    Stroke = Brushes.LightSkyBlue,
                    StrokeThickness = 0,
                    MaxColumnWidth = 5,
                }
            };

            Labels = _sessions.Select(x => x.Date.ToString("HH:mm")).ToArray();

            AvgActive = TimeSpan.FromSeconds(Math.Round(_sessions.Count > 0 ? _sessions.Average(x => x.ActiveTime) : 0, 2)).ToString(TIMEFORMAT);
            AvgResting = TimeSpan.FromSeconds(Math.Round(_sessions.Count > 0 ? _sessions.Average(x => x.RestTime) : 0, 2)).ToString(TIMEFORMAT);

            LabelFormatter = value => Math.Round(TimeSpan.FromSeconds(value).TotalMinutes, 2).ToString();

            DataContext = this;
        }

        private void DateFilter_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textblock = sender as TextBlock;
            switch (textblock?.Text)
            {
                case DateFilters.Today:
                    daysFilter = 0;
                    break;
                case DateFilters.Last7:
                    daysFilter = 7;
                    break;
                case DateFilters.Last30:
                    daysFilter = 30;
                    break;
                default:
                    daysFilter = 0;
                    break;
            }

            RefreshData();
            CreateChart();
        }

        private void GetData()
        {
            var data = _sessionRepository.GetSessionsAsync(daysFilter);
            Task.WaitAll(data);
            _sessions = data.Result;
        }
        private void RefreshData()
        {
            GetData();
            CreateChart();
            CalculateNextBreak();
        }

    }

    public static class DateFilters
    {
        public const string Today = "Today";
        public const string Last7 = "Last 7 days";
        public const string Last30 = "Last 30 days";

    }
}
