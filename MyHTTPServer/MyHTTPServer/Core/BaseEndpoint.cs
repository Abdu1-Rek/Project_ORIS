using System.Net;

namespace MyHttpServer.Core
{
    internal class BaseEndpoint
    {
        public HttpRequestContext Context { get; private set; }

        internal void SetContext(HttpRequestContext context)
        {
            Context = context;
        }
    }
}
