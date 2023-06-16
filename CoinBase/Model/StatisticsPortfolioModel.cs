using CoinBase.Data;
using CoinBase.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Model
{
    class StatisticsPortfolioModel
    {
        //private AddNewModel coinlist;

        public StatisticsPortfolioModel()
        {
            //coinlist.List = CoinGecko.Coins.ToList();
        }
        public double GetPercent24HBalance()
        {
            double balance = 0;

            using (var context = new MyDbContext())
            {
                if (context.Portfolio.Count() > 0)
                {
                    var transactions = context.Portfolio.ToList();
                    for (var i = 0; i < context.Portfolio.Count(); i++)
                    {
                        var items = CoinGecko.Coins.Where(x => x.id == transactions[i].Ids);
                        if (items.Count() == 0)
                        {
                            var portfolioModel = new PortfolioModel();
                            CoinGecko.Coins.Add(portfolioModel.GetCoin(transactions[i].Ids));
                        }
                        var currentPrice = double.Parse(items.First().current_price.ToString().Replace(".", ","));
                        var percent = double.Parse(items.First().price_change_percentage_24h.ToString().Replace(".", ","));
                        var old = (currentPrice*transactions[i].Quantity)/(1 + percent/100);
                        balance += old;
                    }
                }

            }
            return (balance/GetBalancePortfolio()*100)-100; 
        }
        public double GetBalancePortfolio()
        {
            double balance = 0;

            using (var context = new MyDbContext())
            {
                var transactions = context.Portfolio.ToList();
                for (var i = 0; i < context.Portfolio.Count(); i++)
                {                   
                    var price = double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First().current_price.ToString().Replace(".", ","));
                    balance += (price * transactions[i].Quantity);
                }
            }
            return balance;
        }
        public double GetAllTimeProfit()
        {
            double balance = 0;
            
            using (var context = new MyDbContext())
            {
                var transactions = context.Portfolio.ToList();
                for (var i = 0; i < context.Portfolio.Count(); i++)
                {                
                    balance += (transactions[i].AvgBuyPrice * transactions[i].Quantity);
                }
            }
            return GetBalancePortfolio() - balance;
        }
        public Performer GetBestPerformer()
        {
            double maxProfit = 0;
            string image = string.Empty;
            string symbol = string.Empty;
            string name = string.Empty;

            var coin = new Performer();

            using (var context = new MyDbContext())
            {
                var transactions = context.Portfolio.ToList();

                var buyPrice = transactions[0].AvgBuyPrice * transactions[0].Quantity;
                var currentPrice = transactions[0].Quantity * double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().current_price.ToString().Replace(".", ","));
                maxProfit = currentPrice - buyPrice;
                image = CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().image;
                symbol = CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().symbol;

                for (var i = 1; i < context.Portfolio.Count(); i++)
                {
                    buyPrice = transactions[i].AvgBuyPrice * transactions[i].Quantity;
                    currentPrice = transactions[i].Quantity * double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First().current_price.ToString().Replace(".", ","));
                    if(currentPrice - buyPrice > maxProfit)
                    {
                        var item = CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First();
                        maxProfit = currentPrice - buyPrice;
                        image = item.image;
                        symbol = item.symbol;
                        name = item.name;
                    }
                }
            }
            coin.Profit = maxProfit;
            coin.Image = image;
            coin.Symbol = symbol;
            coin.Name = name;
            return coin;
        }
        public Performer GetWorstPerformer()
        {
            double minProfit = 0;
            string image = string.Empty;
            string symbol = string.Empty;
            string name = string.Empty;

            var coin = new Performer();

            using (var context = new MyDbContext())
            {
                var transactions = context.Portfolio.ToList();

                var buyPrice = transactions[0].AvgBuyPrice * transactions[0].Quantity;
                var currentPrice = transactions[0].Quantity * double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().current_price.ToString().Replace(".", ","));
                minProfit = currentPrice - buyPrice;
                image = CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().image;
                symbol = CoinGecko.Coins.Where(x => x.id == transactions[0].Ids).First().symbol;

                for (var i = 1; i < context.Portfolio.Count(); i++)
                {
                    buyPrice = transactions[i].AvgBuyPrice * transactions[i].Quantity;
                    currentPrice = transactions[i].Quantity * double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First().current_price.ToString().Replace(".", ","));
                    if (currentPrice - buyPrice < minProfit)
                    {
                        var item = CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First();
                        minProfit = currentPrice - buyPrice;
                        image = item.image;
                        symbol = item.symbol;
                        name = item.name;
                    }
                }
            }
            coin.Profit = minProfit;
            coin.Image = image;
            coin.Symbol = symbol;
            coin.Name = name;
            return coin;
        }
        public Dictionary<string, double> Get5Element()
        {
            var list = new Dictionary<string, double>();
            using (var context = new MyDbContext())
            {
                if (context.Portfolio.Count() >= 1)
                {
                    foreach(var item in context.Portfolio.ToList())
                    {
                        list.Add(item.Ids, item.Quantity * CoinGecko.Coins.Where(x => x.id == item.Ids).First().current_price);
                    }  
                }
            }
            
            return list;
        }
    }
}
