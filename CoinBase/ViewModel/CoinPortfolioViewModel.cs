using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.DataBase;
using CoinBase.Model;
using System;
using System.Collections.Generic;
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
    class CoinPortfolioViewModel : INotifyPropertyChanged
    {
        private string balance;
        private string quantity;
        private string avgBuyPrice;
        private string symbol;
        private string h24Percent;
        private string totalProfitLoss;
        private string totalProfitLossPercent;
        private string currentPrice;
        private List<TransactionListItem> items;
        private SolidColorBrush totalProfitLossForeground;
        private SolidColorBrush h24PercentForeground;

        public string Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public string AvgBuyPrice
        {
            get => avgBuyPrice;
            set
            {
                avgBuyPrice = value;
                OnPropertyChanged(nameof(AvgBuyPrice));
            }
        }
        public string Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }
        public string H24Percent
        {
            get => h24Percent;
            set
            {
                h24Percent = value;
                OnPropertyChanged(nameof(H24Percent));
            }
        }
        public string TotalProfitLoss
        {
            get => totalProfitLoss;
            set
            {
                totalProfitLoss = value;
                OnPropertyChanged(nameof(TotalProfitLoss));
            }
        }
        public string TotalProfitLossPercent
        {
            get => totalProfitLossPercent;
            set
            {
                totalProfitLossPercent = value;
                OnPropertyChanged(nameof(TotalProfitLossPercent));
            }
        }
        public SolidColorBrush TotalProfitLossForeground
        {
            get => totalProfitLossForeground;
            set
            {
                totalProfitLossForeground = value;
                OnPropertyChanged(nameof(TotalProfitLossForeground));
            }
        }
        public SolidColorBrush H24PercentForeground
        {
            get => h24PercentForeground;
            set
            {
                h24PercentForeground = value;
                OnPropertyChanged(nameof(H24PercentForeground));
            }
        }
        public string CurrentPrice
        {
            get => currentPrice;
            set
            {
                currentPrice = value;
                OnPropertyChanged(nameof(CurrentPrice));
            }
        }

        public List<TransactionListItem> ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }

        public Coin Coin { get; set; }

        public CoinPortfolioViewModel(Coin coin)
        {
            Coin = coin;
            var model = new CoinPortfolioModel(coin);
            var green = new SolidColorBrush(Color.FromRgb(22, 199, 132));
            var red = new SolidColorBrush(Color.FromRgb(234, 57, 67));

            if (model.TotalProfitLoss >= 0) TotalProfitLossForeground = green;
            else TotalProfitLossForeground = red;
            if (coin.price_change_percentage_24h >= 0) H24PercentForeground = green;
            else H24PercentForeground = red;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            Balance = string.Format(nfi, "{0:C}", model.Balance);
            Quantity = model.Quantity.ToString();
            AvgBuyPrice = string.Format(nfi, "{0:C}", model.AvgBuyPrice);
            Symbol = coin.symbol.ToUpper();
            H24Percent = coin.price_change_percentage_24h.ToString("0.00")+"%";
            TotalProfitLossPercent = model.TotalProfitLossPercent.ToString("0.00") + "%";
            TotalProfitLoss = "(" + Filter(model.TotalProfitLoss.ToString("0.00")) + ")";
            CurrentPrice = string.Format(nfi, "{0:C}", coin.current_price);
            ItemsSource = ToTransactionListItem(model.ItemsSource);
        }
        public string Filter(string value)
        {
            if (value[0].ToString() == "-") return value.Insert(1, "$");
            else return "+$" + value;
        }
        public List<TransactionListItem> ToTransactionListItem(List<Transaction> transactions)
        {
            var list = new List<TransactionListItem>();

            foreach (var item in transactions)
            {
                list.Add(new TransactionListItem(item));
            }
            return list;
        }
        public ICommand ComeBack => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "CoinPortfolioView")
                {
                    window.GridChange.Children.RemoveAt(i);
                }
            }
        });
        //public ICommand DeleteTransactionCommand => new DelegateCommand(o =>
        //{
        //    var item = o as Transaction;
        //    using (var context = new MyDbContext())
        //    {
        //        if (context.Transactions.Where(x => x.Date == item.Date).Count() > 1)
        //        {
        //            var itemTransaction = context.Transactions.First(x => x.Date == item.Date);
        //            context.Transactions.Remove(itemTransaction);
        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            var itemPortfolio = context.Portfolio.First(x => x.Date == item.Date);
        //            context.Portfolio.Remove(itemPortfolio);
        //            var itemTransactions = context.Transactions.First(x => x.Date == item.Date);
        //            context.Transactions.Remove(itemTransactions);
        //            context.SaveChanges();
        //        }
        //    }
        //});
        //public ICommand ChangeTransactionCommand => new DelegateCommand(o =>
        //{
        //    MessageBox.Show("Сработало.");
        //});
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
