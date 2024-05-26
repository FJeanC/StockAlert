namespace StockAlert.Contracts
{
    internal interface IStockMonitor
    {
        Task MonitorStock(string symbol, decimal sellPrice, decimal buyPrice);
    }
}
