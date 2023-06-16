using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data.Gecko
{
    public class Coin
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("symbol")]
        public string symbol { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("current_price")]
        public double current_price { get; set; }

        [JsonProperty("market_cap")]
        public double market_cap { get; set; }

        [JsonProperty("market_cap_rank")]
        public int market_cap_rank { get; set; }

        [JsonProperty("fully_diluted_valuation")]
        public double? fully_diluted_valuation { get; set; }

        [JsonProperty("total_volume")]
        public double total_volume { get; set; }

        [JsonProperty("high_24h")]
        public double high_24h { get; set; }

        [JsonProperty("low_24h")]
        public double low_24h { get; set; }

        [JsonProperty("price_change_24h")]
        public double price_change_24h { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double price_change_percentage_24h { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public double market_cap_change_24h { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public double market_cap_change_percentage_24h { get; set; }

        [JsonProperty("circulating_supply")]
        public double? circulating_supply { get; set; }

        [JsonProperty("total_supply")]
        public double? total_supply { get; set; }

        [JsonProperty("max_supply")]
        public double? max_supply { get; set; }

        [JsonProperty("ath")]
        public double ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public double ath_change_percentage { get; set; }

        [JsonProperty("ath_date")]
        public DateTime ath_date { get; set; }

        [JsonProperty("atl")]
        public double atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public double atl_change_percentage { get; set; }

        [JsonProperty("atl_date")]
        public DateTime atl_date { get; set; }

        [JsonProperty("roi")]
        public object roi { get; set; }

        [JsonProperty("last_updated")]
        public string last_updated { get; set; }

        [JsonProperty("price_change_percentage_1h_in_currency")]
        public double? price_change_percentage_1h_in_currency { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency")]
        public double? price_change_percentage_24h_in_currency { get; set; }

        [JsonProperty("price_change_percentage_7d_in_currency")]
        public double? price_change_percentage_7d_in_currency { get; set; }
    }
}