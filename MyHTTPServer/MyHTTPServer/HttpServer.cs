using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using MyHttpServer.Models;


namespace MyHttpServer
{
	internal sealed class HttpServer
	{
		private readonly HttpListener _listener;
		private readonly string _staticDirectoryPath;

		public HttpServer(string[] prefixes, string staticDirectoryPath)
		{
			_listener = new HttpListener();
			foreach (var prefix in prefixes)
			{
				Console.WriteLine($"Server started on {prefix}");
				_listener.Prefixes.Add(prefix);
			}
			_staticDirectoryPath = staticDirectoryPath;
		}

		public async Task StartAsync()
		{
			_listener.Start();
			Console.WriteLine("Server is running...");

			while (_listener.IsListening)
			{
				try
				{
					var context = await _listener.GetContextAsync();
					_ = Task.Run(() => ProcessRequestAsync(context)); // Handle each request asynchronously
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			}
		}

		private async Task ProcessRequestAsync(HttpListenerContext context)
		{
			string relativePath = context.Request.Url.AbsolutePath.TrimStart('/');
			string filePath = Path.Combine(_staticDirectoryPath, string.IsNullOrEmpty(relativePath) ? "index.html" : relativePath);

			if (context.Request.HttpMethod == "POST")
			{
				User user = await GetPostData(context.Request);
				Console.WriteLine($"Получены данные: Name = {user.Login}, Password = {user.Password}");
			}

			if (!File.Exists(filePath))
			{
				filePath = Path.Combine(_staticDirectoryPath, "404.html");
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			}
			else
			{
				context.Response.StatusCode = (int)HttpStatusCode.OK;
			}

			byte[] responseFile;
			try
			{
				responseFile = await File.ReadAllBytesAsync(filePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				responseFile = Encoding.UTF8.GetBytes("500 - Internal Server Error");
			}

			context.Response.ContentType = GetContentType(Path.GetExtension(filePath));
			context.Response.ContentLength64 = responseFile.Length;
			await context.Response.OutputStream.WriteAsync(responseFile, 0, responseFile.Length);
			context.Response.OutputStream.Close();
		}

		private string GetContentType(string? extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException(nameof(extension), "Extension cannot be null.");
			}

			return extension.ToLower() switch
			{
				".html" => "text/html",
				".svg" => "image/svg",
				".css" => "text/css",
				".js" => "application/javascript",
				".jpg" => "image/jpeg",
				".jpeg" => "image/jpeg",
				".png" => "image/png",
				".gif" => "image/gif",
				".webp" => "image/webp",
				_ => "application/octet-stream",
			};
		}

		public void Stop()
		{
			_listener.Stop();
			Console.WriteLine("Server closed");
		}
		
		private async Task<User> GetPostData(HttpListenerRequest request)
		{
			// Чтение содержимого тела запроса
			using var reader = new StreamReader(request.InputStream);
			string body = await reader.ReadToEndAsync();

			// Распарсим данные
			var data = HttpUtility.ParseQueryString(body);

			// Создаем и заполняем объект User
			var user = new User
			{
			Login = data["Login"],
			Password = data["Password"]
			};

			return user;
		}
	}
}
