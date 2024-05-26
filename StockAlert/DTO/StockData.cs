using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert.DTO
{
    internal class StockDataDTO
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
