using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netDumbster.smtp;

namespace SampleSmtp.Tests
{
    [TestClass()]
    public class NotificationServiceTests
    {
        private static SimpleSmtpServer _smtpServer;

        [AssemblyInitialize]
        public static void AppDomainSetup(TestContext context)
            => _smtpServer = SimpleSmtpServer.Start(25);

        [AssemblyCleanup]
        public static void Cleaup() => _smtpServer.Stop();

        [TestMethod()]
        public void NotifyTo_Approved_Test()
        {
            _smtpServer.ClearReceivedEmail();

            new NotificationService().NotifyTo("Approved", "drunkcoding@outlook.com");

            var email = _smtpServer.ReceivedEmail.First();

            Assert.AreEqual(email.FromAddress.Address, "unittest@drunkcoding.net");
            Assert.AreEqual(email.ToAddresses.First().Address, "drunkcoding@outlook.com");
            Assert.IsTrue(email.Data.Contains("Subject: This is the notification for status Approved"));
            Assert.IsTrue(email.MessageParts[0].BodyData.Contains("This is notification email demo drunkcoding.net, pleasse ignore it if you are developers."));
        }

        [TestMethod()]
        public void NotifyTo_Rejected_Test()
        {
            _smtpServer.ClearReceivedEmail();

            new NotificationService().NotifyTo("Rejected", "drunkcoding@outlook.com");

            var email = _smtpServer.ReceivedEmail.First();

            Assert.AreEqual(email.FromAddress.Address, "unittest@drunkcoding.net");
            Assert.AreEqual(email.ToAddresses.First().Address, "drunkcoding@outlook.com");
            Assert.IsTrue(email.Data.Contains("Subject: This is the notification for status Rejected"));
            Assert.IsTrue(email.MessageParts[0].BodyData.Contains("This is notification email demo drunkcoding.net, pleasse ignore it if you are developers."));
        }
    }
}