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


      


        public IActionResult Index()
        {

            var adminViewModel = new AdminViewModel();
            var meals = _dbContext.Meals.Include(m => m.Allergies).ToList(); //meals with all the allergy properties
            var allAllergies = _dbContext.Allergies.ToList();  


            foreach (var meal in meals)
            {
                //var allergyViewModels = new List<AllergyViewModel>();
                var mealAllergies = meal.Allergies;

                //foreach(var mealAllegy in mealAllergies)
                //{
                //    allergyViewModels.Add(new AllergyViewModel()
                //    {
                //        Name = mealAllegy.Name,
                //        Abbreviation = mealAllegy.Abbreviation,
                //        DeleteId = mealAllegy.AllergyId,

                //    });
                //}

                var allergiesAbbrSting = String.Join(",",mealAllergies.Select(m => m.Abbreviation));

                var mealViewModel = new AdminMealViewModel()
                {
                    MealId = meal.MealId,
                    DishName = meal.Title,
                    Description = meal.Description,
                    ImageUrl = meal.ImgUrl,
                    Rating = meal.Rating,
                    AllergiesEditInfo = new AllergiesInlineEditViewModel()
                    {
                        allergyAbbreviations = allergiesAbbrSting,
                        mealId = meal.MealId,
                    },
                    BasePrice = meal.BasePrice,
                    Discount = meal.Discount,
                    SalePrice = meal.BasePrice * (1 - meal.Discount),
                    IsFeatured = meal.IsFeatured,

                    DeleteId = meal.MealId,
                };




                adminViewModel.meals.Add(mealViewModel);
            }

            //foreach(var allergy in allergies)
            //{
            //    var allergyViewModel = new AllergyViewModel()
            //    {
            //        Name = allergy.Name,
            //        Abbreviation = allergy.Abbreviation,
            //    };
            //}




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

        //[HttpGet("/admin/allergiesList")]
        public IActionResult AllergiesList()
        {
            var allergies = _dbContext.Allergies.ToList();
            List<AllergyViewModel> AllergiesListViewModel = new List<AllergyViewModel>();  
            foreach(var allergy in allergies)
            {
                AllergiesListViewModel.Add(new AllergyViewModel()
                {
                    
                    Name = allergy.Name,
                    Abbreviation = allergy.Abbreviation,
                    DeleteId = allergy.AllergyId,
                }); 
            }

            return View(AllergiesListViewModel);
        }


        [HttpPost]
        public IActionResult AllergyCreate(CreateAllergyFormModel createAllergyForm)
        {
            if(ModelState.IsValid)
            {
                var newAllergy = new Allergy()
                {
                    Name = createAllergyForm.Name,
                    Abbreviation = createAllergyForm.Abbreviation,
                };

                _dbContext.Allergies.Add(newAllergy);
                _dbContext.SaveChanges();

                return Ok(new { message="New allergy has been added", data= createAllergyForm });
            }
            else
            {

            return BadRequest(new { message = "Invalid form", data = createAllergyForm });
            }
        }
        [HttpPost]
        public IActionResult AllergyUpdate([FromBody]UpdateAllergyFormModel updateAllergyForm)
        {
            if(ModelState.IsValid)
            {
                //we use the deleteId to track the entity to be update
                var allergyToUpdate = _dbContext.Allergies.Find(updateAllergyForm.DeleteId);

                if(allergyToUpdate != null)
                {
                    allergyToUpdate.Name = updateAllergyForm.Name;  
                    allergyToUpdate.Abbreviation = updateAllergyForm.Abbreviation;

                    _dbContext.SaveChanges();
                    return Ok(new {message = "Allergy is updated", data= allergyToUpdate });
                }
                else
                {
                    return BadRequest(new {message="allergy is not found",  data= updateAllergyForm });
                }

            }
            else
            {
                return BadRequest(new { message = "allergy is not found", data = updateAllergyForm });
            }
        }



        [HttpPost]
        public IActionResult AllergyDelete([FromBody] int id)
        {
           var allergyToDelete = _dbContext.Allergies.Find(id);
            if (allergyToDelete != null) { 
                _dbContext.Allergies.Remove(allergyToDelete); 
                _dbContext.SaveChanges();   
                return Ok(new {message="allergy is deleted",data= allergyToDelete});
            }
            else { return BadRequest(new { message = "allergy not found", data = new { allergyToDelete = id } }); }
        }


        public IActionResult CategoriesList() {
        
            var categoriesList = new List<CategoryViewModel>();
            var categories = _dbContext.Categories;

            foreach (var category in categories)
            {
                categoriesList.Add(new CategoryViewModel
                {
                    Name = category.Name,
                    Description = category.Description,
                    DeleteId = category.CategoryId,
                });
            }

            return View(categoriesList);    
        }

        [HttpPost]
        
        public IActionResult CategoryCreate(CreateCategoryFormModelcs createCategoryForm)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category()
                {
                    Name = createCategoryForm.Name,
                    Description = createCategoryForm.Description??"",
                };

                _dbContext.Categories.Add(newCategory);
                _dbContext.SaveChanges();

                return Ok(new { message = "New category has been added", data = createCategoryForm });
            }
            else
            {

                return BadRequest(new { message = "Invalid form", data = createCategoryForm });
            }
        }

        [HttpPost]
        public IActionResult CategoryUpdate([FromBody] UpdateCategoryFormModel updateCategoryForm)
        {
            if (ModelState.IsValid)
            {
                //we use the deleteId to track the entity to be update
                var categoryToUpdate = _dbContext.Categories.Find(updateCategoryForm.DeleteId);

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = updateCategoryForm.Name;
                    categoryToUpdate.Description = updateCategoryForm.Description;

                    _dbContext.SaveChanges();
                    return Ok(new { message = "Category is updated", data = categoryToUpdate });
                }
                else
                {
                    return BadRequest(new { message = "category is not found", data = updateCategoryForm });
                }

            }
            else
            {
                return BadRequest(new { message = "allergy is not found", data = updateCategoryForm });
            }
        }


        [HttpPost]
        public IActionResult CategoryDelete([FromBody] int id)
        {
            var categoryToDelete = _dbContext.Categories.Find(id);
            if (categoryToDelete != null)
            {
                _dbContext.Categories.Remove(categoryToDelete);
                _dbContext.SaveChanges();
                return Ok(new { message = "category is deleted", data = categoryToDelete });
            }
            else { return BadRequest(new { message = "category not found", data = new { categoryToDelete = id } }); }
        }

        [HttpGet]
        public IActionResult getMealAllergiesEditForm(int id) {
        
            if(id != 0)
            {
                var allAllergies = _dbContext.Allergies.ToList();   

                var meal = _dbContext.Meals.Include(m=>m.Allergies).FirstOrDefault(m=>m.MealId == id);
                
                var allergies = meal.Allergies.ToList();

                var mealAllergiesEditFormViewModel = new MealAllergiesEditFormViewModel();

                mealAllergiesEditFormViewModel.MealId = id;

                foreach(var item in allergies)
                {
                    mealAllergiesEditFormViewModel.MealAllergies.Add(new AllergyViewModel()
                    {
                        Abbreviation = item.Abbreviation,
                        Name = item.Name,
                        DeleteId = item.AllergyId,

                    });
                }

                foreach (var item in allAllergies)
                {
                    mealAllergiesEditFormViewModel.AllAllergies.Add(new AllergyViewModel()
                    {
                        Abbreviation = item.Abbreviation,
                        Name = item.Name,
                        DeleteId = item?.AllergyId,
                    });
                }
                return PartialView("_MealAllergiesEditForm",mealAllergiesEditFormViewModel);
            }
            else
            {
                return BadRequest(new { message = "Meal not found" });
            }
        }
    }


}
