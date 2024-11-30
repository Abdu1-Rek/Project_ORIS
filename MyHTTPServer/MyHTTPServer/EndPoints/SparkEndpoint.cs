using System;
using System.Collections.Generic;
using MyHttpServer.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyHttpServer.EndPoints
{
	internal class SparkEndpoint : BaseEndpoint
	[Get("spark")]
	public void GetSparkPage(HttpRequestContext context, string name)
	{
		Console.WriteLine(name);
	}
}