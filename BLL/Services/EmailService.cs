using BLL.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {
        public IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public void Send(string toEmail, string subject, string body)
        {
            var fromEmail = config["EmailSettings:FromEmail"];
            var password = config["EmailSettings:AppPassword"];
            var host = config["EmailSettings:SmtpServer"];
            var port = int.Parse(config["EmailSettings:Port"]);

            var fromAddress = new MailAddress(fromEmail, "Medispring Hospital");
            var toAddress = new MailAddress(toEmail);

            var msg = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            smtp.Send(msg);
        }

    }
}
