using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data
{
    class Chart
    {
        [JsonProperty("prices")]
        public List<List<double>> prices { get; set; }
    }
}
