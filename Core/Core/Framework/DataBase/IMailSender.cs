namespace ConsoleApp10 // Ensure this matches your actual namespace
{
    public interface IMailSender
    {
        EmailSettings GetConfiguration(string configFilePath);
        void SendEmail(MessageData messageData);
    }
}
