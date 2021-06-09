using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Cryptography.Web
{
	public static class Program
	{
		public static int Main(String[] args)
		{
			Program.CreateHostBuilder(args).Build().Run();
			return 0;
		}

		public static IHostBuilder CreateHostBuilder(String[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}
