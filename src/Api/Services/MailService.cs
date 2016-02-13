// Copyright (c) FireGiant.  All Rights Reserved.

using System.Net;
using System.Net.Mail;

namespace ErrorCode.Api.Services
{
    public class MailService : IMailService
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool RequireSsl { get; set; }

        public void Mail(string from, string to, string subject, string message)
        {
            using (var msg = new MailMessage(from, to, subject, message))
            {
                using (var smtp = new SmtpClient(this.Server, this.Port))
                {
                    smtp.EnableSsl = this.RequireSsl;
                    smtp.Credentials = new NetworkCredential(this.Username, this.Password);

                    smtp.Send(msg);
                }
            }
        }
    }
}