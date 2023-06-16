using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data
{
    class ChartUSD
    {
        [JsonProperty("price")]
        public double? price { get; set; }

        [JsonProperty("volume_24h")]
        public double? volume_24h { get; set; }

        [JsonProperty("market_cap")]
        public double? market_cap { get; set; }

        [JsonProperty("timestamp")]
        public string timestamp { get; set; }
    }
}
