using System.Net;
using System.Net.Mail;

namespace HexaPSA.Tool.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly IMailConfiguration _mailConfiguration;

        public EmailService(IMailConfiguration mailConfiguration)
        {
            _mailConfiguration = mailConfiguration;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_mailConfiguration.Host))
                {
                    smtpClient.Port = _mailConfiguration.Port;
                    smtpClient.Credentials = new NetworkCredential(_mailConfiguration.Username, _mailConfiguration.Password);
                    smtpClient.EnableSsl = _mailConfiguration.UseSSL;
                    smtpClient.UseDefaultCredentials = false;

                    using (var mailMessage = new MailMessage(_mailConfiguration.EmailAddress, toEmail))
                    {
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }
    }
}
