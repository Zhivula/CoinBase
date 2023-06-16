using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data
{
    class HistoricalMarketData
    {
        [JsonProperty("prices")]
        public List<List<double>> prices { get; set; }

        [JsonProperty("market_caps")]
        public List<List<double>> market_caps { get; set; }

        [JsonProperty("total_volumes")]
        public List<List<double>> total_volumes { get; set; }
    }
}
