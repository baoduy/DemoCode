using System.Net.Mail;

namespace SampleSmtp
{
    public class NotificationService
    {
        public void NotifyTo(string status, params string[] emails)
        {
            var email = new MailMessage
            {
                Subject = $"This is the notification for status {status}",
                Body = @"Dear All
This is notification email demo drunkcoding.net, pleasse ignore it if you are developers.

Thanks."
            };

            foreach (var m in emails)
                email.To.Add(m);
           
            new SmtpClient().Send(email);
        }
    }
}
