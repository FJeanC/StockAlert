using Microsoft.Extensions.Configuration;
using StockAlert.Contracts;
using System.Net.Mail;
using System.Net;

namespace StockAlert.Services.Mail
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            //Criar uma verificaçação para ver se os dados de configuração estão válidos

            using var client = new SmtpClient(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]))
            {
                Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["SenderPassword"]),
                EnableSsl = true
            };
            using var message = new MailMessage(emailSettings["SenderEmail"], emailSettings["ReceiverEmail"])
            {
                Subject = subject,
                Body = body
            };
            try
            {
                await client.SendMailAsync(message);
                Console.WriteLine($"Email enviado para: {emailSettings["SenderEmail"]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
