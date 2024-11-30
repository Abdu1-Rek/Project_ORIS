using System.Net;

namespace MyHttpServer.Core
{
    internal class HttpRequestContext
    {
        public HttpListenerRequest Request { get; set; }

        public HttpListenerResponse Response { get; set; }

        public HttpRequestContext(HttpListenerContext context)
        {
            Request = context.Request;
            Response = context.Response;
        }
    }
}
