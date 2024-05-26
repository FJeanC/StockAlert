using System.Text.Json.Serialization;

namespace StockAlert.Models
{
    internal class APIResponse
    {
        public class ApiResponse
        {
            [JsonPropertyName("results")]
            public List<StockPrice> Results { get; set; }
        }
        public class StockPrice
        {
            [JsonPropertyName("regularMarketPrice")]
            public decimal RegularMarketPrice { get; set; }
        }
    }
}
