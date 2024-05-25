using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockAlert.Configuration;
using StockAlert.Contracts;
using StockAlert.Services.Mail;
using StockAlert.Services.Stock;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Exemplo de uso: run.exe PETR4 22.67 22.59");
            return;
        }

        string ativo = args[0];
        if (!decimal.TryParse(args[1].Trim().Replace('.', ','), out decimal precoVenda) ||
            !decimal.TryParse(args[2].Trim().Replace('.', ','), out decimal precoCompra))
        {
            Console.WriteLine("Os preços devem ser valores decimais.");
            // Tem que tratar mais coisas
            return;
        }

        var services = ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();


        var monitor = serviceProvider.GetRequiredService<IStockMonitor>();
        await monitor.MonitorStock(ativo, precoVenda, precoCompra);

        Console.WriteLine("Monitorando o ativo. Pressione [Enter] para sair...");
        Console.ReadLine();
    }

    private static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        IConfiguration configuration = Configuration.LoadConfiguration();
        services.AddSingleton(configuration);

        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<IStockService, StockService>();
        services.AddSingleton<IStockMonitor, MonitorStockService>();

        return services;
    }
}
