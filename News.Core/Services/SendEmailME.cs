
using System.Net.Mail;
using System.Net;

namespace News.Core
{
    public class SendEmailME
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            var SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mohamad1382mhd@gmail.com", "Decortina");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("mohamad1382mhd@gmail.com", "09135377502");
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Send(mail);

        }
    }
}