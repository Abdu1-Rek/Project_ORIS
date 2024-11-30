using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Attributes
{
	internal sealed class GetAttribute : Attribute
	{
		public string Route { get;}
		
		public GetAttribute(string route)
		{
			Route = route;
		}
	}
}