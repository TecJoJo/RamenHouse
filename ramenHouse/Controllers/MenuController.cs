﻿    using Microsoft.AspNetCore.Mvc;
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
            var categories = _dbContext.Categories.Select(e => e.Name).ToList();

            ViewData["category"] = category;

            return View(categories);
        }

        [HttpGet("/menu/GetMenuList/{category?}")]
        public IActionResult GetMenuList(string? category)
        {
            //filter the meals based on the category 
            var meals = new List<Meal>();
            if (category == null || category == "All")
            {

                meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties
            }
            else
            {

                meals = _dbContext.Meals.Include(m => m.Category).Where(e => e.Category.Name == category).ToList();
            }







            
            var menuList = new List<MealViewModel>();



            foreach (var meal in meals)
            {
                var mealViewModel = new MealViewModel()
                {
                    MealId = meal.MealId,
                    Title = meal.Title,
                    Description = meal.Description,
                    ImageUrl = meal.ImgUrl,
                    Rating = meal.Rating,
                    BasePrice = meal.BasePrice,
                    AllergiesShort = string.Join(" ", meal.Allergies.Select(e => e.Abbreviation))
                };



                menuList.Add(mealViewModel);
            }
           







            return PartialView("~/Views/Menu/_menuList.cshtml",menuList);
        }

        public IActionResult Detail(int id)
        {

            var meal = _dbContext.Meals.Where(e => e.MealId == id).Include(m => m.Allergies).Include(m=>m.Category).FirstOrDefault();

            var mealViewModel = new MealViewModel()
            {
                Category = meal.Category.Name,
                Title = meal.Title,
                Description = meal.Description,
                ImageUrl = meal.ImgUrl,
                Rating = meal.Rating,
                BasePrice = meal.BasePrice,
                Discount = meal.Discount,
                AllergiesShort = string.Join(" ", meal.Allergies.Select(e => e.Abbreviation)),
                AllergiesLong = string.Join(" ", meal.Allergies.Select(e => e.Name))
            };
            



            return View(mealViewModel); 
        }
    }
}
