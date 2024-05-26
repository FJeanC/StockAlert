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
        private readonly Dictionary<string, decimal> _lastQuotes;

        public MonitorStockService(IStockService stockService, IEmailService emailService)
        {
            _stockService = stockService;
            _emailService = emailService;
            _lastQuotes = new Dictionary<string, decimal>();
        }
        public async Task MonitorStock(StockDataDTO stockData)
        {
            _timer.Elapsed += async (sender, e) => await CheckStockPrice(stockData);
            _timer.Start();

            await CheckStockPrice(stockData);
        }
        public async Task CheckStockPrice(StockDataDTO stockData)
        {
            try
            {
                decimal quote = await _stockService.GetStockQuote(stockData.Symbol);

                if (quote == InvalidStockQuote)
                {
                    Console.WriteLine($"O ativo {stockData.Symbol} não é válido. Confira a lista de ativos disponíveis no site da brapi.");
                    Console.WriteLine("Encerrando o programa");
                    Environment.Exit(1);
                }

                if (_lastQuotes.ContainsKey(stockData.Symbol) && _lastQuotes[stockData.Symbol] == quote)
                {
                    Console.WriteLine($"A cotação do ativo {stockData.Symbol} não mudou. Nenhum e-mail enviado.");
                    return;
                }
                _lastQuotes[stockData.Symbol] = quote;

                if (quote <= stockData.BuyPrice)
                {
                    await _emailService.SendEmail("Alerta de Compra", $"O preço do ativo {stockData.Symbol} está abaixo do valor de compra configurado. Aconselhamos a compra do ativo. Preço atual:  {quote}");
                }
                else if (quote >= stockData.SellPrice)
                {
                    await _emailService.SendEmail("Alerta de Venda", $"O preço do ativo {stockData.Symbol} está acima do valor de venda configurado. Aconselhamos a venda do ativo. Preço atual:  {quote}");
                }    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar dados na API. Erro: ", ex.ToString());
            }
        }
    }
}
