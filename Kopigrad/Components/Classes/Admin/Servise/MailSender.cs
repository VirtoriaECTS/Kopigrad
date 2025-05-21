using System.Net.Mail;
using System.Net;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class MailSender
    {
        private readonly IConfiguration _config;

        public MailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpHost = _config["Smtp:Host"];
            var smtpPort = int.Parse(_config["Smtp:Port"]);
            var smtpUser = _config["Smtp:Username"];
            var smtpPass = _config["Smtp:Password"];
            var fromEmail = _config["Smtp:From"];
            var enableSsl = bool.Parse(_config["Smtp:EnableSsl"] ?? "true");

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail, "Потверждение почты Копиград"),
                Subject = subject,
                Body = body
            };

            message.To.Add(toEmail);

            await client.SendMailAsync(message);
        }
    }
}
