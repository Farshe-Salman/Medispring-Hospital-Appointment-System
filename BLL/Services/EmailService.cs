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

        public void Send(string ToEmail, string Subject, string Body)
        {
            var from = config["EmailSettings:FromEmail"];
            var pass = config["EmailSettings:AppPassword"];
            var host = config["EmailSettings:SmtpServer"];
            var port = int.Parse(config["EmailSettings:Port"]);

            var msg = new MailMessage(from, ToEmail, Subject, Body);

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(from, pass),
                EnableSsl = true
            };

            smtp.Send(msg);
        }
    }
}
