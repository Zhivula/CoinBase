using CoinBase.Data.Gecko;
using CoinBase.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Model
{
    class CoinPortfolioModel
    {
        public double Balance { get; set; }
        public double Quantity { get; set; }
        public double AvgBuyPrice { get; set; }
        public double TotalProfitLoss { get; set; }
        public double TotalProfitLossPercent { get; set; }
        public List<Transaction> ItemsSource { get; set; }

        public CoinPortfolioModel(Coin coin)
        {
            using (var context = new MyDbContext())
            {
                var item = context.Portfolio.Where(x => x.Ids == coin.id).ToList().FirstOrDefault();
                Balance = item.Quantity*coin.current_price;
                Quantity = item.Quantity;
                AvgBuyPrice = item.AvgBuyPrice;
                TotalProfitLossPercent = (coin.current_price / item.AvgBuyPrice) *100 - 100;
                TotalProfitLoss = (coin.current_price * item.Quantity) - (item.Quantity * item.AvgBuyPrice);

                var itemSource = context.Transactions.Where(x => x.Ids == coin.id).ToList();
                ItemsSource = new List<Transaction>();
                ItemsSource.AddRange(itemSource);
            }
        }
    }
}
