using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyHttpServer.Services
{
	internal class MailService : IMailService
	{
		public async Task SendAsync()
		{
			// отправитель - устанавливаем адрес и отображаемое в письме имя
			MailAddress from = new MailAddress("almaz-nizamov2004@mail.ru", "Almaz");
			// кому отправляем
			MailAddress to = new MailAddress("nizam0v-almaz@yandex.ru");
			// создаем объект сообщения
			MailMessage m = new MailMessage(from, to);
			// тема письма
			m.Subject = "Тест";
			// текст письма
			m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
			// письмо представляет код html
			m.IsBodyHtml = true;
			// адрес smtp-сервера и порт, с которого будем отправлять письмо
			SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
			// логин и пароль
			smtp.Credentials = new NetworkCredential("somemail@mail.ru", "mypassword");
			smtp.EnableSsl = true;
			smtp.Send(m);

			Console.Read();
		}
	}
}