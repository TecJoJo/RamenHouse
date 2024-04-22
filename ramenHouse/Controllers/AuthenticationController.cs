using Microsoft.AspNetCore.Mvc;

namespace ramenHouse.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
