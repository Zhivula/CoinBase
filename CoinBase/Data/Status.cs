using Newtonsoft.Json;

namespace CoinBase.Data
{
    public class Status
    {
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }

        [JsonProperty("error_code")]
        public int? error_code { get; set; }

        [JsonProperty("error_message")]
        public string error_message { get; set; }

        [JsonProperty("elapsed")]
        public int? elapsed { get; set; }

        [JsonProperty("credit_count")]
        public int? credit_count { get; set; }
    }
}
