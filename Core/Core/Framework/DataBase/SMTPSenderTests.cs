using NUnit.Framework;
using ConsoleApp10; // Replace with your actual namespace

[TestFixture]
public class SMTPSenderTests
{
    private SMTPSender smtpSender;

    [SetUp]
    public void Setup()
    {
        smtpSender = new SMTPSender();
    }

    // Add your test methods here
}
