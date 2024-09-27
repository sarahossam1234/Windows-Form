public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public bool UseSSL { get; set; }
    public bool IsBodyHtml { get; set; }

    public EmailSettings(string smtpServer, int smtpPort, string emailAddress, string password, bool useSSL, bool isBodyHtml)
    {
        SmtpServer = smtpServer;
        SmtpPort = smtpPort;
        EmailAddress = emailAddress;
        Password = password;
        UseSSL = useSSL;
        IsBodyHtml = isBodyHtml;
    }

    public EmailSettings() // Default constructor
    {
    }
}
