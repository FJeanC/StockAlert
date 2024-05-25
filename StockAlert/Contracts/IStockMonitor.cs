using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert.Contracts
{
    internal interface IStockMonitor
    {
        Task MonitorStock(string symbol, decimal sellPrice, decimal buyPrice);
    }
}
