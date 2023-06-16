using CoinBase.DataBase;
using CoinBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CoinBase.Data
{
    class CoinPortfolio
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Quantity { get; set; }
        public string PricePerCoin { get; set; }
        public string Price { get; set; }
        public string H24 { get; set; }
        public string Balance { get; set; }
        public Portfolio Transaction { get; set; }
        public SolidColorBrush H24PercentColor { get; set; }

        public ICommand RemoveCoin => new DelegateCommand(o =>
        {
            MessageBox.Show("Успешно!");
            new PortfolioModel().RemoveCoin(Transaction.Name);
        });
    }
}
