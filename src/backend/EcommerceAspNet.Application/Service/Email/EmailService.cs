﻿using MailKit.Net.Smtp;
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

        public async Task SendEmail(string message, string toName, string toEmail)
        {
            var sender = new MimeMessage();

            sender.From.Add(new MailboxAddress(fromName, fromEmail));

            sender.Subject = $"Forget your password {toName}? Reset here!";

            sender.To.Add(new MailboxAddress(toName, toEmail));

            sender.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, true);

                client.Authenticate(fromEmail, );

                client.Send(sender);

                client.Disconnect(true);
            }
        }
    }
}
