using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyHttpServer.Services
{
	internal class MailService : IMailService
	{
		public async void SendAsync(string emailTo, string body, string attachPath = null)
     {
          MailAddress from = new MailAddress("nizam0v.almaz@yandex.ru", "Almaz_Nizamov_yandex");
          MailAddress to = new MailAddress(emailTo);
          MailMessage m = new MailMessage(from, to);
          m.Subject = "Яндекс";
          m.Body = body;
          if (attachPath != null)
          {
               Attachment attachment = new Attachment($"{attachPath}");
               m.Attachments.Add(attachment);
          }
          SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
          smtp.Credentials = new NetworkCredential("nizam0v.almaz@yandex.ru", "hahaha");
          smtp.EnableSsl = true;
          await smtp.SendMailAsync(m);
          Console.WriteLine("Письмо отправлено");
     }
	}
}