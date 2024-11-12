using System.Threading.Tasks;

namespace MyHttpServer.Services
{
    public interface IMailService
    {
        Task SendAsync();
    }
}
