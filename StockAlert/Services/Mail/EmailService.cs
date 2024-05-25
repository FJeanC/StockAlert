using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockAlert.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert.Services.Mail
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmail(string subject, string body)
        {
            Console.WriteLine("Email Mock Enviado", subject, body);
            return Task.CompletedTask;
        }
    }
}
