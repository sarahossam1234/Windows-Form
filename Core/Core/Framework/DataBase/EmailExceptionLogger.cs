using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Log
{
    public class EmailExceptionLogger : IExceptionLog
    {
        private readonly string _emailAddress;

        public EmailExceptionLogger(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public string LogException(Exception exception)
        {
            try
            {
                string exceptionDetails = $"Exception: {exception.Message}\nStack Trace: {exception.StackTrace}";

                MailMessage mail = new MailMessage("your-email@example.com", _emailAddress);
                SmtpClient client = new SmtpClient();
                mail.Subject = "Exception Occurred";
                mail.Body = exceptionDetails;
                client.Send(mail);

                return "An error occurred. A notification email has been sent.";
            }
            catch (Exception ex)
            {
                return $"An error occurred while sending the email: {ex.Message}";
            }
        }
    }
}
