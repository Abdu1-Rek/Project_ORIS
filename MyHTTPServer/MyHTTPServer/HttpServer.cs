using System.Net;

using System.Web;
using MyHttpServer.Services;
using MyHttpServer.Models;


namespace MyHttpServer
{
	public sealed class HttpServer
	{
		private readonly HttpListener _listener;
		private readonly string _staticDirectoryPath;

		public HttpServer(string[] prefixes, string staticDirectoryPath)
		{
			_listener = new HttpListener();
			foreach (var prefix in prefixes)
			{
				Console.WriteLine($"Сервер начал работу на хосте {prefix}");
				_listener.Prefixes.Add(prefix);
			}

			_staticDirectoryPath = staticDirectoryPath;
		}

		public async Task StartAsync()
		{
			_listener.Start();
			while (_listener.IsListening)
			{
				var context = await _listener.GetContextAsync();
				switch (context.Request.HttpMethod)
				{
					case "GET":
						await ProcessRequestAsync(context);
						break;
					case "POST":
						await SendMail(await GetPostData(context.Request), context);
						break;
				}
			}
		}

		private async Task ProcessRequestAsync(HttpListenerContext context)
		{
			var StaticFilesHandler = new StaticFilesHandler();
			var EndpointsHandler = new EndpointsHandler();

			StaticFilesHandler.Successor = EndPointsHandler;

			StaticFilesHandler.HandleRequest(context);
			
			string? relativePath = context.Request.Url?.AbsolutePath.TrimStart('/');
			string filePath = Path.Combine(_staticDirectoryPath,
				string.IsNullOrEmpty(relativePath) ? "index.html" : relativePath);
			
			if (!File.Exists(filePath))
			{
				switch (relativePath)
				{
					case "home-work" or "lol" or "requests" or "bu":
						filePath = Path.Combine(_staticDirectoryPath, "index.html");
						break;
					default:
						filePath = Path.Combine(_staticDirectoryPath, "404.html");
						context.Response.StatusCode = (int)HttpStatusCode.NotFound;
						break;
				}
			}   

			byte[] responseFile = await File.ReadAllBytesAsync(filePath);
			context.Response.ContentType = GetContentType(Path.GetExtension(filePath));
			context.Response.ContentLength64 = responseFile.Length;
			await context.Response.OutputStream.WriteAsync(responseFile, 0, responseFile.Length);
			context.Response.OutputStream.Close();
		}

		
		
		

		public void Stop()
		{
			_listener.Stop();
			Console.WriteLine("Сервер умер");
		}
		
		private async Task<User> GetPostData(HttpListenerRequest request)
		{
			using var reader = new StreamReader(request.InputStream);
			string body = await reader.ReadToEndAsync();
			var data = HttpUtility.ParseQueryString(body);
			var user = new User
			{
				Login = data["Login"]
			};
			return user;
		}
		private async Task SendMail(User user, HttpListenerContext context)
		{
			MailService mailService = new MailService();
			switch (context.Request.Url?.AbsolutePath.TrimStart('/'))
			{
				case "":
					mailService.SendAsync(user.Login, $"Добро пожаловать в систему, Ваша почта: {user.Login}");
					break;
				case "lol":
					mailService.SendAsync(user.Login,$"Ха-ха вы попались, вашу почту {user.Login} теперь знаю Я, Низамов Алмаз");
					break;
				case "requests":
					mailService.SendAsync(user.Login, $"Вы подписались на рассылку ваша почта: {user.Login}");
					break;
				case "home-work":
					mailService.SendAsync(user.Login, "Мое ДЗ от 1сНИКА", Path.Combine(_staticDirectoryPath, "MyHTTPServer.zip"));
					break;
				case "bu":
					mailService.SendAsync(user.Login, $"Бу! Испугался? Не бойся, {user.Login} друг, я тебя не обижу. Иди сюда, иди ко мне, сядь рядом со мной, посмотри мне в глаза. Ты видишь меня? Я тоже тебя вижу. Давай смотреть друг на друга до тех пор, пока наши глаза не устанут. Ты не хочешь? Почему? Что-то не так?");
					break;
			}
		}
	}
}