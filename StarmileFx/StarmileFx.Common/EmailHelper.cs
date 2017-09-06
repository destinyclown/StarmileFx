using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using System;
using StarmileFx.Models.Json;

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
        public static void Send(EmailModel model, string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(model.YoungoName, model.StarmileEamil));
                emailMessage.To.Add(new MailboxAddress("mail", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.163.com", 465, true);
                    string pwd = Encryption.Encryption.ToDecryptDES(model.Password);
                    client.Authenticate(model.StarmileEamil, pwd);

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="email">收件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        public static async Task SendEmailAsync(EmailModel model, string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(model.YoungoName, model.StarmileEamil));
                emailMessage.To.Add(new MailboxAddress("mail", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.163.com", 25, SecureSocketOptions.None).ConfigureAwait(false);
                    await client.AuthenticateAsync(model.StarmileEamil, Encryption.Encryption.ToDecryptDES(model.Password));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
