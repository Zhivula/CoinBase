using CoinBase.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Model
{
    class ListBoxItemPageModel
    {
        public List<ChartCurrencyListView> ItemsSource { get; set; }

        public ListBoxItemPageModel(string name)
        {
            ItemsSource = new List<ChartCurrencyListView>(360);
            var jsonString = APICall_cryptocurrency_listings_latest(360,name);
            var res = JsonConvert.DeserializeObject<Chart>(jsonString);

            for (var i = 0; i < 360; i++)
            {
                ItemsSource.Add(new ChartCurrencyListView(
                    res.prices[i][0].ToString(),
                    res.prices[i][1].ToString()));
            }

        }
        public string APICall_cryptocurrency_listings_latest(int countItems, string name)
        {
            var URL = new UriBuilder($"https://api.coingecko.com/api/v3/coins/{name}/market_chart?vs_currency=usd&days=360&interval=daily");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());

        }
    }
}
