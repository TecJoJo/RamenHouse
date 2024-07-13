using Microsoft.AspNetCore.Mvc;
using ramenHouse.Data;
using ramenHouse.Models;
using ramenHouse.ViewModels;
using System.Diagnostics;

namespace ramenHouse.Controllers
{

    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _DbContext;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext dbContext)
        {
            _logger = logger;
            _DbContext = dbContext;
        }

        public IActionResult Index()
        {
            
            var featuredMealImgURls = _DbContext.Meals.Where(e => e.IsFeatured).OrderByDescending(meal => meal.CreationTime).Select(meal => meal.ImgUrl).Take(6).ToList();



            while (featuredMealImgURls.Count < 6)
            {
                //we add place holder to the list if the list have less then 6 items 
                featuredMealImgURls.Add(AppConstants.placeHolderImgUrl);
            }


            HomeViewModel homeViewModel = new HomeViewModel()
            {
                featuredMealImgs = featuredMealImgURls
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
