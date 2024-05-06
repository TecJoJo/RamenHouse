using Microsoft.AspNetCore.Mvc;

namespace ramenHouse.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
