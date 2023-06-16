using CoinBase.DataBase;
using CoinBase.Model;
using CoinBase.View;
using MetroChart.WPF;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CoinBase.ViewModel
{
    class StatisticsPortfolioViewModel : INotifyPropertyChanged
    {
        private string balance;
        private string allTimeProfit;
        private string bestPerformer;
        private string worstPerformer;
        private string imageSourceBestPerformer;
        private string imageSourceWorstPerformer;
        private string percentChange;
        private string symbolBestPerformer;
        private string symbolWorstPerformer;
        private string nameBestPerformer;
        private string nameWorstPerformer;
        private SolidColorBrush allTimeForeground;
        private SolidColorBrush bestPerformerForeground;
        private SolidColorBrush worstPerformerForeground;
        private SolidColorBrush balanceProfitForeground;

        public string Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public string AllTimeProfit
        {
            get => allTimeProfit;
            set
            {
                allTimeProfit = value;
                OnPropertyChanged(nameof(AllTimeProfit));
            }
        }
        public string BestPerformer
        {
            get => bestPerformer;
            set
            {
                bestPerformer = value;
                OnPropertyChanged(nameof(BestPerformer));
            }
        }
        public string SymbolBestPerformer
        {
            get => symbolBestPerformer;
            set
            {
                symbolBestPerformer = value;
                OnPropertyChanged(nameof(SymbolBestPerformer));
            }
        }
        
        public string NameBestPerformer
        {
            get => nameBestPerformer;
            set
            {
                nameBestPerformer = value;
                OnPropertyChanged(nameof(NameBestPerformer));
            }
        }
        public string WorstPerformer
        {
            get => worstPerformer;
            set
            {
                worstPerformer = value;
                OnPropertyChanged(nameof(WorstPerformer));
            }
        }
        public string SymbolWorstPerformer
        {
            get => symbolWorstPerformer;
            set
            {
                symbolWorstPerformer = value;
                OnPropertyChanged(nameof(SymbolWorstPerformer));
            }
        }
        public string NameWorstPerformer
        {
            get => nameWorstPerformer;
            set
            {
                nameWorstPerformer = value;
                OnPropertyChanged(nameof(NameWorstPerformer));
            }
        }
        public string ImageSourceBestPerformer
        {
            get => imageSourceBestPerformer;
            set
            {
                imageSourceBestPerformer = value;
                OnPropertyChanged(nameof(ImageSourceBestPerformer));
            }
        }
        public string ImageSourceWorstPerformer
        {
            get => imageSourceWorstPerformer;
            set
            {
                imageSourceWorstPerformer = value;
                OnPropertyChanged(nameof(ImageSourceWorstPerformer));
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
        public SolidColorBrush AllTimeForeground
        {
            get => allTimeForeground;
            set
            {
                allTimeForeground = value;
                OnPropertyChanged(nameof(AllTimeForeground));
            }
        }
        public SolidColorBrush BestPerformerForeground
        {
            get => bestPerformerForeground;
            set
            {
                bestPerformerForeground = value;
                OnPropertyChanged(nameof(BestPerformerForeground));
            }
        }
        public SolidColorBrush WorstPerformerForeground
        {
            get => worstPerformerForeground;
            set
            {
                worstPerformerForeground = value;
                OnPropertyChanged(nameof(WorstPerformerForeground));
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

        public StatisticsPortfolioModel model;
        public ObservableCollection<ChartEffect> Data { get; set; }  

        public StatisticsPortfolioViewModel()
        {
            model = new StatisticsPortfolioModel();

            var bestPerformer = model.GetBestPerformer();
            var worstPerformer = model.GetWorstPerformer();

            var redColor = new SolidColorBrush(Color.FromRgb(204, 76, 75));
            var greenColor = new SolidColorBrush(Color.FromRgb(22, 186, 124));

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            if (model.GetAllTimeProfit() >= 0) AllTimeForeground = greenColor;
            else AllTimeForeground = redColor;

            if (bestPerformer.Profit >= 0) BestPerformerForeground = greenColor;
            else BestPerformerForeground = redColor;

            if (worstPerformer.Profit >= 0) WorstPerformerForeground = greenColor;
            else WorstPerformerForeground = redColor;

            if (model.GetPercent24HBalance() >= 0) BalanceProfitForeground = greenColor;
            else BalanceProfitForeground = redColor;

            Balance = string.Format(nfi, "{0:C}", model.GetBalancePortfolio());
            AllTimeProfit = Filter(model.GetAllTimeProfit().ToString("0.00"));
            BestPerformer = Filter(bestPerformer.Profit.ToString("0.00"));
            WorstPerformer = Filter(worstPerformer.Profit.ToString("0.00"));
            ImageSourceBestPerformer = bestPerformer.Image;
            ImageSourceWorstPerformer = worstPerformer.Image;
            PercentChange = model.GetPercent24HBalance().ToString("0.00")+"%";
            SymbolWorstPerformer = worstPerformer.Symbol.ToUpper();
            SymbolBestPerformer = bestPerformer.Symbol.ToUpper();
            NameBestPerformer = bestPerformer.Name;
            NameWorstPerformer = worstPerformer.Name;

            Data = new ObservableCollection<ChartEffect>() {
                new ChartEffect { String = "ADA", Porcent = 49 },
                new ChartEffect { String = "ETH", Porcent = 17 },
                new ChartEffect { String = "BNB", Porcent = 23 },
                new ChartEffect { String = "LTC", Porcent = 8 },
                new ChartEffect { String = "BTC", Porcent = 3}
            };      
        }
        public string Filter(string value)
        {
            if (value[0].ToString() == "-") return value.Insert(1, "$");
            else return "$" + value; 
        }
        public Rect GetRectangle()
        {
            var rect = new Rect();
            rect.Height = 20;
            rect.Width = 100;

            return rect;
        }
        public ICommand ComeBack => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            var j = window.GridChange.Children.OfType<PortfolioView>().FirstOrDefault();
            j.ChangeGridd.Children.Add(new StatisticsPortfolioView());

            for (int i = j.ChangeGridd.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = j.ChangeGridd.Children[i].GetType().Name;
                if (childTypeName == "StatisticsPortfolioView")
                {
                    j.ChangeGridd.Children.RemoveAt(i);
                }
            }
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
    public class ChartEffect
    {
        public int Porcent { set; get; }
        public string String { set; get; }
    }
}
