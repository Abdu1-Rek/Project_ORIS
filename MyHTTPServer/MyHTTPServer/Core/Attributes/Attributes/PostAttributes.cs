using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Attributes
{
	internal sealed class PostAttribute : Attribute
	{
		public string Route { get;}
		
		public PostAttribute(string route)
		{
			Route = route;
		}
	}
}