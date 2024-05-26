namespace StockAlert.Contracts
{
    internal interface IEmailService
    {
        Task SendEmail(string subject, string body);
    }
}
