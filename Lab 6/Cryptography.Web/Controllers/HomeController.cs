using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using Cryptography.Web.Models;

namespace Cryptography.Web.Controllers
{
	public class HomeController: Controller
	{
		private readonly ILogger<HomeController> Logger;

		public HomeController(ILogger<HomeController> logger) => this.Logger = logger;

		public IActionResult Index() => this.View();

		public IActionResult Privacy() => this.View();

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
	}
}
