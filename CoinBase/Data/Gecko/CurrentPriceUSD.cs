using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data.Gecko
{
    public class CurrentPriceUSD
    {
        [JsonProperty("usd")]
        public double usd { get; set; }       
    }
}
