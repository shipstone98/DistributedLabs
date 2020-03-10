using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cryptography.Web.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index() => this.View();

        public IActionResult Login() => this.Challenge(new AuthenticationProperties()
        {
            RedirectUri = "/signin/index"
        }, "Microsoft");
    }
}