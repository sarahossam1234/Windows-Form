using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp10 // Ensure this matches your actual project namespace
{
    public class SMTPSender : IMailSender
    {
        private EmailSettings _emailSettings;

        public EmailSettings GetConfiguration(string configFilePath)
        {
            var encryptedConfig = File.ReadAllText(configFilePath);
            var jsonConfig = Decrypt(encryptedConfig);
            _emailSettings = JsonConvert.DeserializeObject<EmailSettings>(jsonConfig);
            return _emailSettings; // Return the configuration if needed
        }

        public void SendEmail(MessageData message)
        {
            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.EmailAddress, _emailSettings.Password);
                smtpClient.EnableSsl = _emailSettings.UseSSL;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.EmailAddress),
                    Subject = message.Subject,
                    Body = message.Content,
                    IsBodyHtml = _emailSettings.IsBodyHtml
                };

                foreach (var to in message.ToEmails)
                {
                    mailMessage.To.Add(to);
                }
                foreach (var cc in message.CcEmails)
                {
                    mailMessage.CC.Add(cc);
                }

                smtpClient.Send(mailMessage);
            }
        }

        private string Decrypt(string encryptedText)
        {
            // Implement your decryption logic here
            return Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText));
        }
    }
}
