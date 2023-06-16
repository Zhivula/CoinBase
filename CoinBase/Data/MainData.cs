using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinBase.Data
{
    public class MainData
    {
        [JsonProperty("data")]
        public List<Data> data { get; set; }

        [JsonProperty("status")]
        public Status status { get; set; }
    }
}
