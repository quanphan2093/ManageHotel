using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ManageHotel.Config
{
    public class EmailService
    {
        private readonly EmailConfig _emailConfig;
        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", _emailConfig.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
            await client.AuthenticateAsync(_emailConfig.SenderEmail, _emailConfig.SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
