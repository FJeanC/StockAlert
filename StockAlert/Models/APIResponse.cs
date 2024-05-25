using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert.Models
{
    internal class APIResponse
    {
        public class ApiResponse
        {
            public List<StockPrice> results { get; set; }
        }
        public class StockPrice
        {
            public decimal regularMarketPrice { get; set; }
        }
    }
}
