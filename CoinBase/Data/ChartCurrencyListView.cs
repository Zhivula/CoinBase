using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.Data
{
    class ChartCurrencyListView
    {
        public string Price { get; set; }
        public string Volume_24h { get; set; }
        public string Market_cap { get; set; }
        public string Timestamp { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public ChartCurrencyListView(string timestamp, string price)
        {
            Price = price;
            Timestamp = timestamp;
        }
    }
}
