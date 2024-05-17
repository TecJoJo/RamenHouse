using Microsoft.AspNetCore.Mvc;

namespace ramenHouse.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
