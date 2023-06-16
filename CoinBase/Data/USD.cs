using Newtonsoft.Json;

namespace CoinBase.Data
{
    public class USD
    {
        [JsonProperty("price")]
        public double? price { get; set; }

        [JsonProperty("volume_24h")]
        public double? volume_24h { get; set; }

        [JsonProperty("percent_change_1h")]
        public double? percent_change_1h { get; set; }

        [JsonProperty("percent_change_24h")]
        public double? percent_change_24h { get; set; }

        [JsonProperty("percent_change_7d")]
        public double? percent_change_7d { get; set; }

        [JsonProperty("market_cap")]
        public double? market_cap { get; set; }

        [JsonProperty("market_cap_dominance")]
        public double? market_cap_dominance { get; set; }

        [JsonProperty("fully_diluted_market_cap")]
        public double? fully_diluted_market_cap { get; set; }

        [JsonProperty("last_updated")]
        public string last_updated { get; set; }
    }
}
