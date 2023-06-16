using CoinBase.Data.Gecko;
using CoinBase.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Web;
using System.Net.Http;
using System.Net.WebSockets;
using System.Media;

namespace CoinBase.Data
{
    public static class CoinGecko
    {
        public static List<Coin> Coins { get; set; }
        public static List<Item> FullList { get; set; }
        public static async void UpDateList()
        {
            await Task.Run(() =>
            {
                var URL = new UriBuilder("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d&locale=en");
                var client = new WebClient();
                client.Headers.Add("Accepts", "application/json");
                client.Headers.Add("User-Agent: Other");
                var js = client.DownloadString(URL.ToString());

                var res = JsonConvert.DeserializeObject<List<Coin>>(js);
                Coins = new List<Coin>();
                for (var i = 0; i < 100; i++)
                {
                    Coins.Add(res[i]);
                }
                UpDateListFull();
            });
        }
        public static async void UpDateListFull()
        {
            await Task.Run(() =>
            {
                var URL = new UriBuilder("https://api.coingecko.com/api/v3/coins/list?include_platform=false");
                var client = new WebClient();
                client.Headers.Add("Accepts", "application/json");
                var jsonString = client.DownloadString(URL.ToString());
                var res = JsonConvert.DeserializeObject<List<Item>>(jsonString);
                FullList = new List<Item>();
                for (var i = 0; i < res.Count(); i++)
                {
                    FullList.Add(new Item() { id = res[i].id, name = res[i].name, symbol = res[i].symbol });
                }
            });
        }
    }
    public class Item
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
