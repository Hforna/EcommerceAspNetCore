using MailKit.Net.Smtp;
using MimeKit;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Service.Email
{
    public class EmailService
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _name;

        public EmailService(string password, string email, string name)
        {
            _email = email;
            _password = password;
            _name = name;
        }

        public async Task SendEmail(string message, string toEmail, string toName = " ")
        {
            var sender = new MimeMessage();

            sender.From.Add(new MailboxAddress(_name, _email));

            sender.Subject = "Hello";

            sender.To.Add(new MailboxAddress(toName, toEmail));

            sender.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);

                client.Authenticate(_email, _password);

                await client.SendAsync(sender);

                client.Disconnect(true);
            }
        }
    }
}
