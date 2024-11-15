using MyHttpServer.Models;
using MyHttpServer.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyHttpServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var mailService = new MailService();
            //await mailService.SendAsync();

            AppConfig? config = null;
            if (File.Exists("config.json"))
            {
                var fileConfig = await File.ReadAllTextAsync("config.json");
                config = JsonSerializer.Deserialize<AppConfig>(fileConfig);
            }
            else
            {
                Console.WriteLine("файл конфигурации сервера 'config.json' не найден");
                config = new AppConfig();
            }

            var prefixes = new[] { $"http://{config.Domain}:{config.Port}/" };
            string path = Path.Combine(Directory.GetCurrentDirectory(), config.StaticDirectoryPath);
            var server = new HttpServer(prefixes, path);

            await server.StartAsync();
        }
    }
}
