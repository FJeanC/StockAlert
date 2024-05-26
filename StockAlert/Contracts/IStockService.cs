namespace StockAlert.Contracts
{
    internal interface IStockService
    {
        Task<decimal> GetStockQuote(string symbol);
    }
}
