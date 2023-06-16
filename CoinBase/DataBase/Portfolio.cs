using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBase.DataBase
{
    class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public string Ids { get; set; }
        public double Quantity { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double AvgBuyPrice { get; set; }
    }
}
