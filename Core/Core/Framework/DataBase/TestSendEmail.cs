using NUnit.Framework; // Ensure you have NUnit referenced
using ConsoleApp10; // Replace with your actual namespace

namespace ConsoleApp10 // Ensure this matches your project's namespace
{
    [TestFixture]
    public class TestSendEmail
    {
        private SMTPSender smtpSender;

        [SetUp]
        public void Setup()
        {
            smtpSender = new SMTPSender();
        }

        [Test]
        public void TestSendEmailMethod()
        {
            // Your test implementation here
        }
    }
}
