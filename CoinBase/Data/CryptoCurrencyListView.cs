using System.Windows.Media;

namespace CoinBase.Data
{
    public class CryptoCurrencyListView
    {
        public string Cmc_rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Circulating_supply { get; set; }
        public string Percent_change_24h { get; set; }
        public string Percent_change_7d { get; set; }
        public string Market_cap { get; set; }
        public SolidColorBrush Percent_change_24h_Color { get; set; }
        public SolidColorBrush Percent_change_7d_Color { get; set; }
        public Data D { get; set; }
        public string ImageSource { get; set; }

        public CryptoCurrencyListView(string cmc_rank, string symbol, string name, string price, string circulating_supply, string percent_change_24h, string percent_change_7d, string market_cap, SolidColorBrush percent_change_24h_Color, SolidColorBrush percent_change_7d_Color, Data maindata)
        {
            Cmc_rank = cmc_rank;
            Symbol = symbol;
            Name = name;
            Price = price;
            Circulating_supply = circulating_supply;
            Percent_change_24h = percent_change_24h;
            Percent_change_7d = percent_change_7d;
            Market_cap = market_cap;
            Percent_change_24h_Color = percent_change_24h_Color;
            Percent_change_7d_Color = percent_change_7d_Color;
            D = maindata;
            ImageSource = $"https://s2.coinmarketcap.com/static/img/coins/32x32/{D.id}.png";
        }
    }
}
