using Newtonsoft.Json;

namespace CoinBase.Data
{
    public class Data
    {
        [JsonProperty("id")]
        public int? id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("symbol")]
        public string symbol { get; set; }

        [JsonProperty("slug")]
        public string slug { get; set; }

        [JsonProperty("cmc_rank")]
        public int? cmc_rank { get; set; }

        [JsonProperty("num_market_pairs")]
        public int? num_market_pairs { get; set; }

        [JsonProperty("circulating_supply")]
        public double? circulating_supply { get; set; }

        [JsonProperty("total_supply")]
        public double? total_supply { get; set; }

        [JsonProperty("max_supply")]
        public double? max_supply { get; set; }

        [JsonProperty("tags")]
        public string[] tags { get; set; }

        [JsonProperty("quote")]
        public Quote quote { get; set; }
    }
}