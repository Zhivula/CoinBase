using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CoinBase.Data;
using CoinBase.Data.Gecko;
using Newtonsoft.Json;

namespace CoinBase.Model
{
    class CryptoCurrencyModel : INotifyPropertyChanged
    {
        //private string API_KEY = "025df3bc-79d6-45d3-83c3-76594a50f8d4";
        private List<Coin> items;
        public List<Coin> ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }

        public CryptoCurrencyModel()
        {
            ItemsSource = new List<Coin>();
            //var jsonString = APICall_cryptocurrency_listings_latest(countItems);
            //var res = JsonConvert.DeserializeObject<MainData>(jsonString);
            //SolidColorBrush percent_change_24h_color;
            //SolidColorBrush percent_change_7d_color;

            //for (var i = 0; i < countItems; i++)
            //{
                //if (res.data[i].quote.usd.percent_change_24h > 0) percent_change_24h_color = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                //else percent_change_24h_color = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                //if (res.data[i].quote.usd.percent_change_7d > 0) percent_change_7d_color = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                //else percent_change_7d_color = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                ItemsSource.AddRange(CoinGecko.Coins);
                    //res.data[i].cmc_rank.ToString(),
                    //res.data[i].symbol,
                    //res.data[i].name,
                    //"$" + ((double)res.data[i].quote.usd.price).ToString("0.00"),
                    //((double)res.data[i].circulating_supply).ToString("0.00") + " " + res.data[i].symbol,
                    //((double)res.data[i].quote.usd.percent_change_24h).ToString("0.00") + "%",
                    //((double)res.data[i].quote.usd.percent_change_7d).ToString("0.00") + "%",
                    //"$" + ((double)res.data[i].quote.usd.market_cap).ToString("0,000."),
                    //percent_change_24h_color,
                    //percent_change_7d_color,
                    //res.data[i]));
            //}
        }
        //public string APICall_cryptocurrency_listings_latest(int countItems)
        //{
        //    var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        //    URL.Query = $"start=1&limit={countItems}&convert=USD";

        //    var client = new WebClient();
        //    client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        //    client.Headers.Add("Accepts", "application/json");
        //    return client.DownloadString(URL.ToString());

        //}
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
