using Microsoft.Extensions.Logging;
using StockAlert.Contracts;

namespace StockAlert.Services.Stock
{
    internal class MonitorStockService : IStockMonitor
    {
        private readonly IStockService _stockService;
        private readonly IEmailService _emailService;
        private static readonly System.Timers.Timer _timer = new(60000);

        public MonitorStockService(IStockService stockService, IEmailService emailService)
        {
            _stockService = stockService;
            _emailService = emailService;
        }
        public async Task MonitorStock(string symbol, decimal sellPrice, decimal buyPrice)
        {
            _timer.Elapsed += async (sender, e) => await CheckStockPrice(symbol, sellPrice, buyPrice);
            _timer.Start();

            await CheckStockPrice(symbol, sellPrice, buyPrice);
        }
        private async Task CheckStockPrice(string symbol, decimal sellPrice, decimal buyPrice)
        {
            try
            {
                var quote = await _stockService.GetStockQuote(symbol);

                if (quote <= buyPrice)
                {
                    await _emailService.SendEmail("Alerta de Compra", $"O ativo {symbol} atingiu o preço de compra: {quote}");
                }
                else if (quote >= sellPrice)
                {
                    await _emailService.SendEmail("Alerta de Venda", $"O ativo {symbol} atingiu o preço de venda: {quote}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.ToString());
            }
        }
    }
}
