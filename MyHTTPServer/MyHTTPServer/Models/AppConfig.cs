namespace MyHttpServer.Models
{
    public class AppConfig
    {
        public string Domain { get; set; } = "localhost";
        public int Port { get; set; } = 7574;
        public string StaticDirectoryPath { get; set; } = "public";
    }
}
