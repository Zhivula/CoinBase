using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.DataBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Model
{
    class AddNewModel
    {
        public ObservableCollection<Coin> List { get; set; }

        public AddNewModel()
        {
            List = new ObservableCollection<Coin>();
            //var jsonString = API_CoinGecko();
            //var res = JsonConvert.DeserializeObject<List<Coin>>(jsonString);

            for (var i = 0; i < CoinGecko.Coins.Count(); i++)
            {
                List.Add(CoinGecko.Coins[i]);
            }
        }
        public string API_CoinGecko()
        {
            var URL = new UriBuilder($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }
        public void AddNew(string id, double quantity, string name, DateTime date, string symbol, double pricePerCoin, double fee, string notes)
        {
            using (var context = new MyDbContext())
            {
                var portfolioCoin = new Portfolio()
                {
                    Ids = id,
                    Quantity = quantity,
                    Name = name,
                    Symbol = symbol,
                    AvgBuyPrice = pricePerCoin,
                };
                var transaction = new Transaction()
                {
                    Ids = id,
                    Quantity = quantity,
                    Name = name,
                    Date = date,
                    Symbol = symbol,
                    PricePerCoin = pricePerCoin,
                    Fee = fee,
                    Notes = notes,
                };
                if (context.Portfolio.Select(x=>x.Name).Contains(name))
                {
                    var item = context.Portfolio.Where(x => x.Name == name).Single();
                    item.AvgBuyPrice = (item.Quantity * item.AvgBuyPrice + quantity * pricePerCoin) / (quantity + item.Quantity);
                    item.Quantity += quantity;
                    
                    context.Entry(item).Property(x => x.Quantity).IsModified = true;
                    context.Entry(item).Property(x => x.AvgBuyPrice).IsModified = true;    
                    
                    context.Transactions.Add(transaction);

                    context.SaveChanges();
                }
                else
                {
                    context.Portfolio.Add(portfolioCoin);
                    context.SaveChanges();
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
        }

    }
}
