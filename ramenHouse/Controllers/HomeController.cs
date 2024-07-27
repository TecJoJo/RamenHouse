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
            //initialize empty featuredmeals and onSellMeals

            var featuredMeals = new List<MealViewModel>();  
            var onSellMeals = new List<MealViewModel>();

            //we fiilter out the featured meals, where we take only the first 6 pictures, if there is less than 6 pictures we 
            //fill the empty slot with the placeholder picture 
            var dbFeaturedMeals = _DbContext.Meals.Where(e => e.IsFeatured).OrderByDescending(meal => meal.CreationTime).Select(meal => new {imgUrl = meal.ImgUrl, mealId = meal.MealId}).Take(6).ToList();

            foreach(var dbFeaturedMeal in dbFeaturedMeals)
            {
                var featuredMeal = new MealViewModel()
                {
                    ImageUrl = dbFeaturedMeal.imgUrl,
                    MealId = dbFeaturedMeal.mealId,
                };

                featuredMeals.Add(featuredMeal);
              
            }

            //we add dummy meal with no mealid and placeholder's url
            while (featuredMeals.Count < 6)
            {
                var featuredMeal = new MealViewModel()
                {
                    ImageUrl = AppConstants.placeHolderImgUrl,
                    
                };

                featuredMeals.Add(featuredMeal);

            }





            //with the same logic we filter out 3 meals with discount in such an order the meal with most discount is on top of the list
            var dbOnSellMeals = _DbContext.Meals.Where(m=>m.Discount>0).OrderByDescending(m=>(double)m.Discount).Select(meal => new { imgUrl = meal.ImgUrl, mealId = meal.MealId }).Take(3).ToList();


            foreach (var dbOnSellMeal in dbOnSellMeals)
            {
                var onSellMeal = new MealViewModel()
                {
                    ImageUrl = dbOnSellMeal.imgUrl,
                    MealId = dbOnSellMeal.mealId,
                };

                onSellMeals.Add(onSellMeal);

            }

            while (onSellMeals.Count < 3)
            {
                var onSellMeal = new MealViewModel()
                {
                    ImageUrl = AppConstants.placeHolderImgUrl,

                };

                onSellMeals.Add(onSellMeal);
            }


            // we initialize the HomeViewModel here
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                featuredMeals = featuredMeals,
                onSellMeals = onSellMeals
                
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
