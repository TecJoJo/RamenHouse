using Microsoft.AspNetCore.Http;
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
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public AdminController(ApplicationDbContext dbContext, IConfiguration config, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _config = config;
            _env = env;
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

        [HttpGet]
        public IActionResult MealCreate()
        {
            var allergies = _dbContext.Allergies.ToList();
            ViewData["allergies"] = allergies;

            return View();
        }
        [HttpPost]
        public IActionResult MealCreate(MealCreateFormModel mealCreateForm)
        {
            if(ModelState.IsValid)
            {
                

                //store the uploaded image 
                var imgStoragePath = Path.Combine(_env.WebRootPath, _config["imgStoragePath"]);
                if (!Directory.Exists(imgStoragePath))
                {
                    Directory.CreateDirectory(imgStoragePath);
                }

                string filename = Path.GetFileNameWithoutExtension(mealCreateForm.imgFile.FileName);
                string fileExtension = Path.GetExtension(mealCreateForm.imgFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + fileExtension;

                var filePath = Path.Combine(imgStoragePath,filename);
                // we extract this relative path which can be using in the html. 
                string relativeFilePath = Path.Combine(_config["imgStoragePath"], Path.GetFileName(filePath)).Replace("\\", "/");

                using (var stream = System.IO.File.Create(filePath))
                {
                    mealCreateForm.imgFile.CopyTo(stream);
                }

                //add the new meal infomation into the database
                var newMeal = new Meal()
                {
                    Title = mealCreateForm.Name,
                    Description = mealCreateForm.Description,
                    ImgUrl = relativeFilePath,
                    BasePrice = mealCreateForm.BasePrice,
                    CreationTime = DateTime.Now,

                    //we temparatyly add fixed category as the time is running out
                    CategoryId = _dbContext.Categories.FirstOrDefault().CategoryId


                };

                //add allergies into the meal's allergy's collection

                foreach(int allergyId in mealCreateForm.AllergyIds)
                {
                    Allergy newAllergy = _dbContext.Allergies.Find(allergyId);
                    if (newAllergy != null)
                    {
                        newMeal.Allergies.Add(newAllergy);
                    }

                }

                _dbContext.Meals.Add(newMeal);
                _dbContext.SaveChanges();


                
            }

            return RedirectToAction("MealCreate");
        }
    }
}
