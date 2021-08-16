using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
	public class Program
	{
		private static int startPortNumber = 32768;

		public static void Main(string[] args)
		{
			var webHost = CreateWebHost();
			webHost.RunAsync().GetAwaiter().GetResult();
		}

		private static IWebHost CreateWebHost()
		{
			return new WebHostBuilder()
				.UseStartup<Startup>()
				.UseKestrel()
				.UseUrls(GenerateUrls())
				.Build();

		}

		private static string[] GenerateUrls()
		{
			return Enumerable.Range(startPortNumber, 10)
				.Select(x => $"http://0.0.0.0:{x}")
				.ToArray();
		}
	}
}
