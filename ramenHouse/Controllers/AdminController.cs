using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ramenHouse.Data;
using ramenHouse.FormModels;
using ramenHouse.Models;
using ramenHouse.ViewModels;
using ramenHouse.ViewModels.Admin;

namespace ramenHouse.Controllers
{
    [Authorize]
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


        //var source = meals.Select(meal =>
        //{
        //    var salePrice = meal.BasePrice * (1 - meal.Discount);
        //    return
        //    new
        //    {
        //        mealId = meal.MealId,
        //        dishName = meal.Title,
        //        description = meal.Description,
        //        imageUrl = meal.ImageUrl,
        //        rating = meal.Rating,
        //        allergies = meal.AllergiesLong,
        //        basePrice = meal.BasePrice,
        //        discount = meal.Discount,
        //        salePrice = salePrice,
        //        isFeatured = meal.IsFeatured,

        //        deleteId = meal.MealId

        //    };
        //});


        public IActionResult Index()
        {

            var adminViewModel = new AdminViewModel();
            var meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties



            foreach (var meal in meals)
            {
                var mealViewModel = new AdminMealViewModel()
                {
                    MealId = meal.MealId,
                    DishName = meal.Title,
                    Description = meal.Description,
                    ImageUrl = meal.ImgUrl,
                    Rating = meal.Rating,
                    Allergies = string.Join(",", meal.Allergies.Select(e => e.Name)),
                    BasePrice = meal.BasePrice,
                    Discount = meal.Discount,
                    SalePrice = meal.BasePrice * (1 - meal.Discount),
                    IsFeatured = meal.IsFeatured,

                    DeleteId = meal.MealId,
                };



                adminViewModel.meals.Add(mealViewModel);
            }




            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult MealUpdate([FromBody] UpdateMealFormModel form)
        {

            if (ModelState.IsValid)
            {


                var mealId = form.MealId;
                

                    Meal meal = _dbContext.Meals.Find(mealId);



                    meal.Title = form.DishName;
                    meal.Description = form.Description;
                    meal.ImgUrl = form.ImageUrl;
                    meal.Rating = form.Rating;
                    meal.BasePrice = form.BasePrice;
                    meal.Discount = form.Discount;
                    meal.IsFeatured = form.IsFeatured;
                    _dbContext.SaveChanges();



                
            return Ok();
            }
            else
            {
                return BadRequest();
            }
            

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
            if (ModelState.IsValid)
            {


                string relativeFilePath = "/imgs/application/placeholder-image.jpg";
                //only if user provided img we process the img, otherwise defualt message will be tied to the meal
                if (mealCreateForm.imgFile != null)
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

                    var filePath = Path.Combine(imgStoragePath, filename);
                    // we extract this relative path which can be using in the html. 
                    relativeFilePath = Path.Combine(_config["imgStoragePath"], Path.GetFileName(filePath)).Replace("\\", "/");
                    relativeFilePath = "/" + relativeFilePath;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        mealCreateForm.imgFile.CopyTo(stream);
                    }
                }

                //add the new meal infomation into the database
                var newMeal = new Meal()
                {
                    Title = mealCreateForm.Name,
                    Description = mealCreateForm.Description,
                    ImgUrl = relativeFilePath,
                    BasePrice = mealCreateForm.BasePrice,
                    CreationTime = DateTime.Now,
                    IsFeatured = mealCreateForm.IsFeatured,
                    //we temparatyly add fixed category as the time is running out
                    CategoryId = _dbContext.Categories.FirstOrDefault().CategoryId


                };

                //add allergies into the meal's allergy's collection

                foreach (int allergyId in mealCreateForm.AllergyIds)
                {
                    Allergy newAllergy = _dbContext.Allergies.Find(allergyId);
                    if (newAllergy != null)
                    {
                        newMeal.Allergies.Add(newAllergy);
                    }

                }

                _dbContext.Meals.Add(newMeal);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {

                return RedirectToAction("MealCreate");
            }

        }

        [HttpPost]
        public IActionResult MealDelete([FromBody] int id)
        {
            //int.TryParse(id, out int mealId);
            var meal = _dbContext.Meals.Find(id);
            if (meal != null)
            {
                var imgPath = meal.ImgUrl;
                //we dont want to delete the placeholder image
                if (imgPath != AppConstants.placeHolderImgUrl)
                {

                    var absoluteImgPath = Path.Combine(_env.WebRootPath, imgPath.TrimStart('/').Replace("/", "\\"));
                    if (System.IO.File.Exists(absoluteImgPath))

                    {
                        try
                        {

                            System.IO.File.Delete(absoluteImgPath);
                            Console.WriteLine($"{absoluteImgPath} is deleted successfully");
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"file {imgPath} is not found");
                    }
                }

                _dbContext.Meals.Remove(meal);
                _dbContext.SaveChanges();

                return Ok(new { status = 200, message = "Meal deleted successfully", success = true });
            }

            return NotFound(new { status = 404, message = "Meal not found", error = true });
        }
    }
}
