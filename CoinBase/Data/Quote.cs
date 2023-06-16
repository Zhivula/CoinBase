using Newtonsoft.Json;

namespace CoinBase.Data
{
    public class Quote
    {
        [JsonProperty("USD")]
        public USD usd { get; set; }
    }
}
