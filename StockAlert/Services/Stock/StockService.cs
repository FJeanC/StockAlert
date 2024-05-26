using Microsoft.Extensions.Configuration;
using StockAlert.Contracts;
using System.Text.Json;
using static StockAlert.Models.APIResponse;
using static StockAlert.Constants.StockConstant;
using static System.Net.WebRequestMethods;

namespace StockAlert.Services.Stock
{
    internal class StockService : IStockService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public StockService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<decimal> GetStockQuote(string symbol)
        {
            string url = CreateURL(symbol);
            try
            {
                var response = await this._httpClient.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse>(response);

                if (data != null && data.Results.Count > 0)
                {
                    return data.Results[0].RegularMarketPrice;
                }
                Console.WriteLine($"Erro. Dados da API de cotação para o ativo {symbol} vieram vazio ou nulo");
                return InvalidStockQuote;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro ao solicitar a cotação do ativo {symbol}: {httpEx.Message}");
                return InvalidStockQuote;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Erro ao desserializar a resposta da API para o ativo {symbol}: {jsonEx.Message}");
                return InvalidStockQuote;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao obter a cotação do ativo {symbol}: {ex.Message}");
                return InvalidStockQuote;
            }
        }

        public string CreateURL(string symbol)
        {
            var apiSettings = _configuration.GetSection("ApiSettings");
            return $"{apiSettings["ApiUrl"]}{symbol}?token={apiSettings["ApiKey"]}";
        }
    }
}
