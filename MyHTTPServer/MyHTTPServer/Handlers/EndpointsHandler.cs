using System.Net;
using System.Reflection;
using MyHttpServer.Attributes;

namespace MyHttpServer.Handlers
{
	internal class EndpointsHandler : Handler
	{
		
		private readonly Dictionary<string, List<(HttpMethod method, MethodInfo handler, Type endpointType)>> _routers = new();
		public override void HandleRequest(HttpListenerContext context)
		{
			
			if (true)
			{
				
			}
			else if (Successor != null)
			{
				Successor.HandleRequest(context);
			}
		}
		
		private void RegisterEndpointsFromAssemblies(Assembly[] assemblies)
		{
			foreach (var assembly in assemblies)
			{
				var endpointsTypes = assembly.GetTypes()
					.Where(t => typeof(BaseEndpoint).IsAssignableFrom(t) && !t.IsAbstract);
				foreach (var endpointType in endpointsTypes)
				{
					var methods = endpointType.GetMethods();
					foreach (var method in methods)
					{
						// Register GET endpoints
						var getAttribute = method.GetCustomAttribute<GetAttribute>();
						if(getAttribute != null)
						{
							// Register the route with the GET method
							RegisterRoute(getAttribute.Route, HttpMethod.Get, method, endpointType);
						}
						
						// Register POST endpoints
						var postAttribute = method.GetCustomAttribute<PostAttribute>();
						if(postAttribute != null)
						{
							// Register the route with the POST method
							RegisterRoute(postAttribute.Route, HttpMethod.Post, method, endpointType);
						}
					}
				}
			}
		}
		
		private void RegisterRoute(string route, HttpMethod method, MethodInfo handler, Type endpointType) 
		{
			if(!_routers.ContainsKey(route))
			{
				_routers[route] = new();
			}
			
			_routers[route].Add((method, handler, endpointType));
		}
	}
}