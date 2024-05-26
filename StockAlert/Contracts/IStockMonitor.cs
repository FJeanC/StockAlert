using StockAlert.DTO;

namespace StockAlert.Contracts
{
    internal interface IStockMonitor
    {
        Task MonitorStock(StockDataDTO stockData);
    }
}
