using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ramenHouse.Models;
using ramenHouse.ViewModels;

namespace ramenHouse.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public MenuController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties
            var categories = _dbContext.Categories.Select(e=>e.Name).ToList();
            var menuViewModel = new MenuViewModel();
            foreach (var meal in meals){
                var mealViewModel = new MealViewModel()
                {
                    Title = meal.Title,
                    Description = meal.Description,
                    ImageUrl = meal.ImgUrl,
                    Rating = meal.Rating,
                    Price = meal.Price,
                    Allergies = string.Join(" ", meal.Allergies.Select(e => e.Abbreviation))
                };



                menuViewModel.Meals.Add(mealViewModel);
                menuViewModel.Categories = categories;
            }



            

            

            return View(menuViewModel);
        }
    }
}
