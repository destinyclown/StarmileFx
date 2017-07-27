using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace StarmileFx.Common
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="email">收件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        public static void Send(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Youngo雅戈", "youngo163cn@163.com"));
            emailMessage.To.Add(new MailboxAddress("mail", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.163.com", 465, true);
                client.Authenticate("youngo163cn@163.com", "crz15915120067");

                client.Send(emailMessage);
                client.Disconnect(true);

            }
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="email">收件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Youngo雅戈", "youngo163cn@163.com"));
            emailMessage.To.Add(new MailboxAddress("mail", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.163.com", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.AuthenticateAsync("youngo163cn@163.com", "crz15915120067");
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
        }

    }
}
