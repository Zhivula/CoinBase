using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.DataBase;
using CoinBase.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CoinBase.Model
{
    class PortfolioModel
    {
        public PortfolioModel()
        {

        }
        public List<CoinPortfolio> GetCoinsPortfolio()
        {
            //var coinlist = new AddNewModel();

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var list = new List<CoinPortfolio>();
            using (var context = new MyDbContext())
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
                    var price = double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First().current_price.ToString().Replace(".", ","));
                    var h24 = double.Parse(CoinGecko.Coins.Where(x => x.id == transactions[i].Ids).First().price_change_percentage_24h.ToString().Replace(".", ","));

                    var item = new CoinPortfolio()
                    {
                        Price = string.Format(nfi, "{0:C}", price),
                        Symbol = transactions[i].Symbol.ToUpper(),
                        Transaction = transactions[i],
                        Balance = string.Format(nfi, "{0:C}", (price * transactions[i].Quantity)),
                        H24 = h24.ToString("0.00") + "%",
                        H24PercentColor = GetH24Color(h24),
                        PricePerCoin = string.Format(nfi, "{0:C}", transactions[i].AvgBuyPrice)
                    };
                    list.Add(item);
                }
            }
            return list;
        }
        public double GetLastPrice(string ids)
        {
            var URL = new UriBuilder($"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=usd");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            var jsonString = client.DownloadString(URL.ToString());

            var parseTree = JsonConvert.DeserializeObject<JObject>(jsonString);

            double result = 0;

            if (parseTree.Properties().Count() != 0)
            {
                var result1 = parseTree.Properties().First();
                var result2 = result1.First();
                var result3 = result2.First();
                var result4 = result3.First().ToObject<object>();
                result = double.Parse(result4.ToString());
            }

            return result; 
        }
        //public void GetPrices(string name, DateTime time,int days, int count, ref Dictionary<DateTime, double> list, double quantity)
        //{
        //    using (var context = new MyDbContext())
        //    {
        //        if (context.Portfolio.Count() > 0)
        //        {
        //            var earlyDate = time.AddDays(-days);
        //            var jsonString = GetHistoricalData(name, GetUnixTime(earlyDate), GetUnixTime(time));
        //            var res = JsonConvert.DeserializeObject<HistoricalMarketData>(jsonString);

        //            for (var i = 0; i < res.prices.Count(); i++)
        //            {
        //                list[i] += res.prices[i][1]* quantity;
        //            }
        //        }
        //    }
        //}
        //public List<double> GetPrices(string name, DateTime time)
        //{
        //    var list = new List<double>();
        //    using (var context = new MyDbContext())
        //    {
        //        if (context.Portfolio.Count() > 0)
        //        {
        //            var earlyDate = context.Transactions.Select(x => x.Date).ToList().First();
        //            var jsonString = GetHistoricalData(name, GetUnixTime(earlyDate), GetUnixTime(time));
        //            var res = JsonConvert.DeserializeObject<HistoricalMarketData>(jsonString);

        //            for (var i = 0; i < res.prices.Count(); i++)
        //            {
        //                list.Add(res.prices[i][1]);
        //            }
        //        }
        //    }
        //    return list;
        //}
        //public int GetCountPrices(string name, DateTime time)
        //{
        //    int count = 0;
        //    using (var context = new MyDbContext())
        //    {
        //        if (context.Portfolio.Count() > 0)
        //        {
        //            var earlyDate = context.Transactions.Select(x => x.Date).ToList().First();
        //            var jsonString = GetHistoricalData(name, GetUnixTime(earlyDate), GetUnixTime(time));
        //            var res = JsonConvert.DeserializeObject<HistoricalMarketData>(jsonString);

        //            return res.prices.Count();
        //        }
        //    }
        //    return count;
        //}
        public List<ChartPriceDate> GetDataChart(int days, DateTime time, int count) 
        {
            var list = new List<ChartPriceDate>(count);
            if (days == 1) for (var i = 0; i < count; i++) list.Add(new ChartPriceDate() { Time = time.AddMinutes(5 * (i - count)), Price = 0 });
            else for (var i = 0; i < count; i++) list.Add(new ChartPriceDate() { Time = time.AddHours(i - count), Price = 0 });

            using (var context = new MyDbContext())
            {
                if (context.Portfolio.Count() > 0)
                {
                    var earlyDate = time.AddDays(-days);
                    foreach(var item in context.Portfolio)
                    {
                        var jsonString = GetHistoricalData(item.Ids, GetUnixTime(earlyDate), GetUnixTime(time));
                        var res = JsonConvert.DeserializeObject<HistoricalMarketData>(jsonString);

                        if (res.prices.Count > count) res.prices = res.prices.Take(count).ToList();

                        for (var i = 0; i < res.prices.Count(); i++)
                        {
                             list[i].Price += (res.prices[i][1]*item.Quantity);
                        }
                    }
                }
            }
            return list;
        }
        public Coin GetCoin(string ids)
        {
            var URL = new UriBuilder($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={ids}&order=market_cap_desc&sparkline=false");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            var jsonString = client.DownloadString(URL.ToString());
            var res = JsonConvert.DeserializeObject<List<Coin>>(jsonString);
            return res.FirstOrDefault();
        }
        public string GetHistoricalData(string name, long start, long finish)
        {
            var URL = new UriBuilder($"https://api.coingecko.com/api/v3/coins/{name}/market_chart/range?vs_currency=usd&from={start}&to={finish}");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }
        /// <summary>
        /// Удаление элемента из базы данных через имя элемента.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveCoin(string name)
        {
            using (var context = new MyDbContext())
            {
                var itemPortfolio = context.Portfolio.FirstOrDefault(x => x.Name == name);
                context.Portfolio.Remove(itemPortfolio);
                var itemTransaction = context.Transactions.Where(x => x.Name == name).ToList();
                context.Transactions.RemoveRange(itemTransaction);
                context.SaveChanges();
            }
        }
        public long GetUnixTime(DateTime time)
        {
            long unixTime = ((DateTimeOffset)time).ToUnixTimeSeconds();
            return unixTime;
        }
        public SolidColorBrush GetH24Color(double h24)
        {
            var redColor = new SolidColorBrush(Color.FromRgb(204, 76, 75));
            var greenColor = new SolidColorBrush(Color.FromRgb(22, 186, 124));

            if (h24 >= 0) return greenColor;
            else return redColor;
        }
    }
}
