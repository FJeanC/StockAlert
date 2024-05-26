using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace StockAlert.Configuration
{
    internal static class Configuration
    {
        public static IConfigurationRoot LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                .AddJsonFile("Configuracao.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
