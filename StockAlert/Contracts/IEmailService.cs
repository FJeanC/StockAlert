using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert.Contracts
{
    internal interface IEmailService
    {
        Task SendEmail(string subject, string body);
    }
}
