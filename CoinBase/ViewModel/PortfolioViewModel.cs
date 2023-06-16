using CoinBase.Data;
using CoinBase.DataBase;
using CoinBase.Model;
using CoinBase.View;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CoinBase.ViewModel
{
    class PortfolioViewModel : INotifyPropertyChanged
    {
        public PortfolioModel model;
        private string balance;
        private string percentChange;
        private SolidColorBrush balanceProfitForeground;

        public List<CoinPortfolio> CoinList { get; set; }

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
        public string Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public string PercentChange
        {
            get => percentChange;
            set
            {
                percentChange = value;
                OnPropertyChanged(nameof(PercentChange));
            }
        }
        public SolidColorBrush BalanceProfitForeground
        {
            get => balanceProfitForeground;
            set
            {
                balanceProfitForeground = value;
                OnPropertyChanged(nameof(BalanceProfitForeground));
            }
        }
        public PortfolioViewModel()
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
      
            model = new PortfolioModel();

            CoinList = new List<CoinPortfolio>();

            var statisticsModel = new StatisticsPortfolioModel();
            var redColor = new SolidColorBrush(Color.FromRgb(204, 76, 75));
            var greenColor = new SolidColorBrush(Color.FromRgb(22, 186, 124));
            var percent24HBalance = statisticsModel.GetPercent24HBalance();

            CoinList.AddRange(model.GetCoinsPortfolio());

            Plot = GetPlotModel();

            Balance = string.Format(nfi, "{0:C}", statisticsModel.GetBalancePortfolio());

            if (percent24HBalance >= 0) BalanceProfitForeground = greenColor;
            else BalanceProfitForeground = redColor;

            PercentChange = percent24HBalance.ToString("0.00") + "%";
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
            
            var list = model.GetDataChart(days,DateTime.Now, GetCount(days));
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(list[i].Time), list[i].Price));
                }
            }

            plot.Series.Add(line);
            plot.TextColor = OxyColors.White;
            plot.PlotAreaBorderThickness = new OxyThickness(1,0,0,1);
            plot.PlotAreaBorderColor = OxyColors.White;
            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Transparent };
            plot.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Gray, IntervalLength = 30 };
            plot.Axes.Add(valueAxis);


            return plot;
        }
        private int GetCount(int days)
        {
            int count = 288;
            if (days == 1) count = 288;
            else if (days == 7) count = 168;
            else if (days == 30) count = 720;
            else if (days == 60) count = 1440;
            else if (days == 90) count = 2160;
            return count;
        }
        public ICommand AddNewCommand => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.GridChange.Children.Add(new AddNewView());
        });
        public ICommand StatisticsCommand => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            var j = window.GridChange.Children.OfType<PortfolioView>().FirstOrDefault();
            j.ChangeGridd.Children.Add(new StatisticsPortfolioView());
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
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
