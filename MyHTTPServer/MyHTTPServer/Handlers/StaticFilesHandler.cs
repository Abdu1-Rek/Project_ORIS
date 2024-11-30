using System.Net;
using System.Net.Http;
using System.IO;
using System;
using System.Linq;

namespace MyHttpServer.Handlers
{
	internal class StaticFilesHandler : Handler
	{
		private readonly string _staticDirectoryPath = "/public";
		
		public override void HandleRequest(HttpListenerContext context)
		{
			bool isGet = context.Request.HttpMethod.Equals("Get", StringComparison.OrdinalIgnoreCase);
			string[] arr = context.Request.Url?.AbsolutePath.Split('.');
			bool IsFile =arr.Length == 2;
			
			if (isGet && IsFile)
			{
				string? relativePath = context.Request.Url?.AbsolutePath.TrimStart('/');
				string filePath = Path.Combine(_staticDirectoryPath,
					string.IsNullOrEmpty(relativePath) ? "index.html" : relativePath);
				
				if (!File.Exists(filePath))
				{
					filePath = Path.Combine(_staticDirectoryPath, "404.html");
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				}
			
				byte[] responseFile = File.ReadAllBytes(filePath);
				context.Response.ContentType = GetContentType(Path.GetExtension(filePath));
				context.Response.ContentLength64 = responseFile.Length;
				context.Response.OutputStream.Write(responseFile, 0, responseFile.Length);
			}
			else if (Successor != null)
			{
				Successor.HandleRequest(context);
			}
		}
		
		private string GetContentType(string? extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException(nameof(extension), "null extenion.");
			}

			return extension.ToLower() switch
			{
				".html" => "text/html",
				".css" => "text/css",
				".js" => "application/javascript",
				".jpg" => "image/jpeg",
				".jpeg" => "image/jpeg",
				".svg" => "image/svg+xml",
				".ico" => "image/x-icon",
				".bmp" => "image/bmp",
				".webp" => "image/webp",
				".png" => "image/png",
				".gif" => "image/gif",
				_ => "application/octet-stream",
			};
		}
	}
}