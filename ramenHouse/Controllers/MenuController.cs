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

        public IActionResult Index(string? category)
        {
            //filter the meals based on the category 
            var meals = new List<Meal>();
            if (category ==null || category == "All")
            {

            meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties
            }
            else
            {

            meals = _dbContext.Meals.Include(m => m.Category).Where(e => e.Category.Name == category).ToList();
            }


            




            var categories = _dbContext.Categories.Select(e=>e.Name).ToList();
            var menuViewModel = new MenuViewModel();

            //check the meal's length, if the the result is found, then we return normal view, if it no result is found, then we add infomation into the viewModel and render the view with the infomaton

           
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
            }
                menuViewModel.Categories = categories;



            

            

            return View(menuViewModel);
        }
    }
}
