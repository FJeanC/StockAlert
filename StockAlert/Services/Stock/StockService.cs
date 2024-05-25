using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockAlert.Contracts;
using StockAlert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static StockAlert.Models.APIResponse;

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
            var apiSettings = _configuration.GetSection("ApiSettings");
            string url = $"{apiSettings["ApiUrl"]}{symbol}?token={apiSettings["ApiKey"]}";

            try
            {
                var response = await this._httpClient.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<ApiResponse>(response);

                if (data != null)
                {
                    return data.results[0].regularMarketPrice;
                }
                return -1; // Mudar
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
