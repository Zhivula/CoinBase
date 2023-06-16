using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CoinBase.Data.Gecko
{
    public class CoinFormat
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Current_price { get; set; }
        public string Market_cap { get; set; }
        public string Market_cap_rank { get; set; }
        public string Fully_diluted_valuation { get; set; }
        public string Total_volume { get; set; }
        public string High_24h { get; set; }
        public string Low_24h { get; set; }
        public string Price_change_24h { get; set; }
        public string Price_change_percentage_24h { get; set; }
        public string Market_cap_change_24h { get; set; }
        public string Market_cap_change_percentage_24h { get; set; }
        public string Circulating_supply { get; set; }
        public string Total_supply { get; set; }
        public string Max_supply { get; set; }
        public string Ath { get; set; }
        public string Ath_change_percentage { get; set; }
        public DateTime Ath_date { get; set; }
        public string Atl { get; set; }
        public string Atl_change_percentage { get; set; }
        public DateTime Atl_date { get; set; }
        public object Roi { get; set; }
        public string Last_updated { get; set; }
        public string Price_change_percentage_1h_in_currency { get; set; }
        public string Price_change_percentage_24h_in_currency { get; set; }
        public string Price_change_percentage_7d_in_currency { get; set; }

        public double Avg24hPercent { get; set; }
        public double Circulating_supply_Percent { get; set; }
        public string Circulating_supply_Percent_String { get; set; }

        public SolidColorBrush Percent_change_24h_Color { get; set; }
        public SolidColorBrush Percent_change_7d_Color { get; set; }

        public string VolumeMarketCap { get; set; }

        public CoinFormat(Coin coin)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var green = new SolidColorBrush(Color.FromRgb(22, 199, 132));
            var red = new SolidColorBrush(Color.FromRgb(234, 57, 67));

            Id = coin.id;
            Symbol = coin.symbol.ToString().ToUpper();
            Name = coin.name.ToString();
            Image = coin.image.ToString();
            Current_price = string.Format(nfi, "{0:C}", coin.current_price);
            Market_cap = string.Format(nfi, "{0:C0}", coin.market_cap);
            Market_cap = Market_cap.Replace(","," ");
            Market_cap_rank = coin.market_cap_rank.ToString();
            Fully_diluted_valuation = coin.fully_diluted_valuation.ToString();
            Total_volume = string.Format(nfi, "{0:C}", coin.total_volume);
            High_24h = string.Format(nfi, "{0:C}", coin.high_24h);
            Low_24h = string.Format(nfi, "{0:C}", coin.low_24h);
            Price_change_24h = string.Format(nfi, "{0:C}", coin.price_change_24h);
            Price_change_percentage_24h = coin.price_change_percentage_24h.ToString("0.##%");
            Market_cap_change_24h = string.Format(nfi, "{0:C}", coin.market_cap_change_24h);
            Market_cap_change_percentage_24h = coin.market_cap_change_percentage_24h.ToString("0.##") + "%";
            Circulating_supply = ((double)coin.circulating_supply).ToString("0.00");
            Total_supply = coin.total_supply.ToString();
            Max_supply = coin.max_supply.ToString();
            Ath = string.Format(nfi, "{0:C}", coin.ath);
            Ath_change_percentage = coin.ath_change_percentage.ToString();
            Ath_date = coin.ath_date;
            Atl = string.Format(nfi, "{0:C}", coin.atl);
            Atl_change_percentage = coin.atl_change_percentage.ToString();
            Atl_date = coin.atl_date;
            Roi = coin.roi;
            Last_updated = coin.last_updated.ToString();
            Price_change_percentage_1h_in_currency = coin.price_change_percentage_1h_in_currency.ToString();
            Price_change_percentage_24h_in_currency = coin.price_change_percentage_24h_in_currency.ToString();
            Price_change_percentage_7d_in_currency = coin.price_change_percentage_7d_in_currency.ToString();

            VolumeMarketCap = (coin.total_volume / coin.market_cap).ToString("0.##");

            if (double.TryParse(Price_change_percentage_1h_in_currency, out double p1h))
            {
                Price_change_percentage_1h_in_currency = p1h.ToString("0.##") + "%";
            }

            if (double.TryParse(Price_change_percentage_24h_in_currency, out double p24h))
            {
                Price_change_percentage_24h_in_currency = p24h.ToString("0.##") + "%";
            }

            if (double.TryParse(Price_change_percentage_7d_in_currency, out double p7d))
            {
                Price_change_percentage_7d_in_currency = p7d.ToString("0.##") + "%";
            }

            if (coin.price_change_percentage_24h_in_currency >= 0) Percent_change_24h_Color = green;
            else Percent_change_24h_Color = red;
            if (coin.price_change_percentage_7d_in_currency >= 0) Percent_change_7d_Color = green;
            else Percent_change_7d_Color = red;

            Avg24hPercent = ((coin.current_price - coin.low_24h) / (coin.high_24h - coin.low_24h))*150;//150 - ширина полоски

            if (coin.max_supply != null && coin.total_supply != null)
            {
                Circulating_supply_Percent = (double)((coin.circulating_supply / coin.max_supply) * 100);
                Circulating_supply_Percent_String = Circulating_supply_Percent.ToString("00") + "%";
            }
        }
    }
}
