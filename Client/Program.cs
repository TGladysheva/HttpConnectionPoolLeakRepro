using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Logging.Abstractions;
using Vostok.Logging.File;
using Vostok.Logging.File.Configuration;

namespace Client
{
	class Program
	{
		private static readonly int StartPortNumber = 32768;
		private static HttpClient _client;
		private static ILog _log;

		static void Main(string[] args)
		{
			_log = new FileLog(new FileLogSettings {FilePath = "log"});

			using var eventSourceListener = new HttpEventListener(_log);
			{
				_client = new HttpClient();

				foreach(var url in GenerateUrls())
					SendAsync(new Uri(url)).GetAwaiter().GetResult();

				// Waiting for DefaultPooledConnectionIdleTimeout and several cleaningTimer (from HttpConnectionPoolManager) iterations.
				Thread.Sleep(TimeSpan.FromMinutes(5));

				GC.Collect();
				GC.WaitForPendingFinalizers();

				Console.Out.WriteLine("Finish");
			}

			Task.Delay(-1).GetAwaiter().GetResult();
		}

		private static IEnumerable<string> GenerateUrls()
		{
			return Enumerable.Range(StartPortNumber, 10)
				.Select(x => $"http://127.0.0.1:{x}/");
		}

		private static async Task SendAsync(Uri uri)
		{
			using(var response = await _client.GetAsync(uri))
			{
			}
		}
	}
}