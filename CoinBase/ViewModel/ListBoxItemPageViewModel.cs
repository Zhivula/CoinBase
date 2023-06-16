using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.DataBase;
using CoinBase.Model;
using CoinBase.View;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CoinBase.ViewModel
{
    class ListBoxItemPageViewModel : INotifyPropertyChanged
    {
        PortfolioModel portfolioModel;
        private CoinFormat currency;

        public CoinFormat Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }
        public string ImageSource { get; set; }

        private PlotModel plot;
        public PlotModel Plot
        {
            get => plot;
            set
            {
                plot = value;
                OnPropertyChanged(nameof(Plot));
            }
        }


        public ListBoxItemPageViewModel(CoinFormat element)
        {
            portfolioModel = new PortfolioModel();
            Currency = element;
            ImageSource = element.Image;
            Plot = GetPlotModel(30);
        }
        public PlotModel GetPlotModel(int days = 1)
        {
            var plot = new PlotModel();

            var line = new OxyPlot.Series.LineSeries()
            {
                Title = "USD",
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 2,
                MarkerSize = 1,
                MarkerType = MarkerType.Circle
            };

            var dictionary = GetDataChart(Currency.Id, DateTime.Now, days);

            if (dictionary.Count > 0)
            {
                for (int i = 0; i < dictionary.Count(); i++)
                {
                    line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dictionary.Keys.ToList()[i]), dictionary.Values.ToList()[i]));
                }
            }

            plot.Series.Add(line);
            plot.TextColor = OxyColors.White;
            plot.PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
            plot.PlotAreaBorderColor = OxyColors.White;

            var min = DateTimeAxis.ToDouble(dictionary.Keys.ToList().First());
            var max = DateTimeAxis.ToDouble(dictionary.Keys.ToList().Last());

            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Transparent, IntervalLength = 80, StringFormat = "d", Maximum = max, Minimum = min };
            plot.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Gray, IntervalLength = 30 };
            plot.Axes.Add(valueAxis);


            return plot;
        }
        public Dictionary<DateTime, double> GetDataChart(string name, DateTime time, int days = 1)
        {
            var list = new Dictionary<DateTime, double>();
            using (var context = new MyDbContext())
            {
                if (context.Portfolio.Count() > 0)
                {
                    var earlyDate = time.AddDays(-days);
                    var jsonString = portfolioModel.GetHistoricalData(name, portfolioModel.GetUnixTime(earlyDate), portfolioModel.GetUnixTime(time));
                    var res = JsonConvert.DeserializeObject<HistoricalMarketData>(jsonString);

                    for (var i = 0; i < res.prices.Count(); i++)
                    {
                        list.Add(GetFromUnixToDateTime(long.Parse(res.prices[i][0].ToString())),res.prices[i][1]);
                    }
                }
            }
            return list;
        }
        public static DateTime GetFromUnixToDateTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime/1000).DateTime.ToUniversalTime();
        }

        public ICommand ComeBack => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "ListBoxItemPageView")
                {
                    window.GridChange.Children.RemoveAt(i);
                }
            }
        });
        public ICommand Button7dCommand => new DelegateCommand(o =>
        {
            Plot = GetPlotModel(7);
        });
        public ICommand Button30dCommand => new DelegateCommand(o =>
        {
            Plot = GetPlotModel(30);
        });
        public ICommand Button60dCommand => new DelegateCommand(o =>
        {
            Plot = GetPlotModel(60);
        });
        public ICommand Button90dCommand => new DelegateCommand(o =>
        {
            Plot = GetPlotModel(90);
        });
        public ICommand Button1YCommand => new DelegateCommand(o =>
        {
            Plot = GetPlotModel(365);
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}