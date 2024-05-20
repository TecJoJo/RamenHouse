using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ramenHouse.FormModels;
using ramenHouse.Models;
using ramenHouse.ViewModels;

namespace ramenHouse.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {

            var adminViewModel = new AdminViewModel();
            var meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties



            foreach (var meal in meals)
            {
                var mealViewModel = new MealViewModel()
                {
                    MealId = meal.MealId,
                    Title = meal.Title,
                    Description = meal.Description,
                    ImageUrl = meal.ImgUrl,
                    Rating = meal.Rating,
                    AllergiesLong = string.Join(",", meal.Allergies.Select(e => e.Name)),
                    IsFeatured = meal.IsFeatured,
                    BasePrice = meal.BasePrice,
                    Discount = meal.Discount,

                };



                adminViewModel.meals.Add(mealViewModel);
            }




            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult MealUpdate([FromBody] UpdateMealFormModel form)
        {
            var mealId = form.mealId;
            Meal meal = _dbContext.Meals.Find(mealId);



            meal.Title = form.dishName;
            meal.Description = form.description;
        meal.ImgUrl = form.imageUrl;
            meal.Rating = form.rating;
            meal.BasePrice = form.basePrice;
            meal.Discount = form.discount;
            meal.IsFeatured = form.isFeatured;
            _dbContext.SaveChanges();
        


            return RedirectToAction("Index");   
        }
    }
}
