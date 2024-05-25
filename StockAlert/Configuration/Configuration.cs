using System.IO;
using Microsoft.Extensions.Configuration;

namespace StockAlert.Configuration
{
    internal static class Configuration
    {
        public static IConfigurationRoot LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuracao.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
