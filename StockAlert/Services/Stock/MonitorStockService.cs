using StockAlert.Contracts;
using StockAlert.DTO;
using static StockAlert.Constants.StockConstant;

namespace StockAlert.Services.Stock
{
    internal class MonitorStockService : IStockMonitor
    {
        private readonly IStockService _stockService;
        private readonly IEmailService _emailService;
        private const int OneMinuteInMilliseconds = 60000;
        private static readonly System.Timers.Timer _timer = new(OneMinuteInMilliseconds);

        public MonitorStockService(IStockService stockService, IEmailService emailService)
        {
            _stockService = stockService;
            _emailService = emailService;
        }
        public async Task MonitorStock(StockDataDTO stockData)
        {
            _timer.Elapsed += async (sender, e) => await CheckStockPrice(stockData);
            _timer.Start();

            await CheckStockPrice(stockData);
        }
        private async Task CheckStockPrice(StockDataDTO stockData)
        {
            try
            {
                var quote = await _stockService.GetStockQuote(stockData.Symbol);

                if (quote != InvalidStockQuote)
                {
                    if (quote <= stockData.BuyPrice)
                    {
                        await _emailService.SendEmail("Alerta de Compra", $"O ativo {stockData.Symbol} atingiu o preço de compra: {quote}");
                    }
                    else if (quote >= stockData.SellPrice)
                    {
                        await _emailService.SendEmail("Alerta de Venda", $"O ativo {stockData.Symbol} atingiu o preço de venda: {quote}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.ToString());
            }
        }
    }
}
