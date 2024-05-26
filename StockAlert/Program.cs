using System;
using System.ComponentModel.DataAnnotations;
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
            Console.WriteLine("O programa requer 3 argumentos. <NOME_DO_ATIVO> <PRECO_VENDA> <PRECO_COMPRA>");
            return;
        }

        if (!IsUserArgumentValid(args, out string ativo, out decimal precoVenda, out decimal precoCompra))
        {
            Console.WriteLine("Finalizando o programa. Digite valores válidos como argumento do progama");
            return;
        }
        Console.WriteLine("Monitorando o ativo. Pressione [Enter] para sair...");

        var services = ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();


        var monitor = serviceProvider.GetRequiredService<IStockMonitor>();
        await monitor.MonitorStock(ativo, precoVenda, precoCompra);

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

    private static bool IsUserArgumentValid(string[] args, out string ativo, out decimal precoVenda, out decimal precoCompra)
    {
        precoVenda = 0;
        precoCompra = 0;
        ativo = args[0].Trim();

        if (string.IsNullOrWhiteSpace(ativo))
        {
            Console.WriteLine("O nome do ativo não pode ser vazio");
            return false;
        }

        if (!decimal.TryParse(args[1].Trim().Replace('.', ','), out precoVenda) ||
            !decimal.TryParse(args[2].Trim().Replace('.', ','), out precoCompra))
        {
            Console.WriteLine("Os valores de compra e venda devem ser um número decimal válido.");
            return false;
        }
        if (precoVenda <= precoCompra)
        {
            Console.WriteLine("O preco de compra não deve ser menor que o preço de venda.");
            return false;
        }

        return true;
    }
}
