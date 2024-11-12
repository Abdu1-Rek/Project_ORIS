using System.Net;
using System.Text;

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
            while (_listener.IsListening)
            {
                var context = await _listener.GetContextAsync();
                await ProcessRequestAsync(context);
            }
        }

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            string relativePath = context.Request.Url?.AbsolutePath.TrimStart('/');
            string filePath = Path.Combine(_staticDirectoryPath, string.IsNullOrEmpty(relativePath) ? "index.html" : relativePath);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(_staticDirectoryPath, "404.html");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            byte[] responseFile = await File.ReadAllBytesAsync(filePath);
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
                ".css" => "text/css",
                ".js" => "application/javascript",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

        public void Stop()
        {
            _listener.Stop();
            Console.WriteLine("Server closed");
        }
    }
}
