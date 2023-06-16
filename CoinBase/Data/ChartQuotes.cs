using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data
{
    class ChartQuotes
    {
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }

        [JsonProperty("quote")]
        public ChartQuote quote { get; set; }
    }
}
