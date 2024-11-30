using System.Net;

namespace MyHttpServer.Handlers
{
	internal abstract class Handler
	{
		public Handler? Successor { get; set; }

		public abstract void HandleRequest(HttpListenerContext context);
	}
}