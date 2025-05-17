using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ramenHouse.Data;
using ramenHouse.Models;
using ramenHouse.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ramenHouse.FormModels;
using Microsoft.AspNetCore.Identity;

namespace ramenHouse.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _DbContext;

        public AuthenticationController(ApplicationDbContext DbCOntext)
        {
            _DbContext = DbCOntext;
        }
        //login page
        [HttpGet]
        public IActionResult Index()
        {



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel loginForm)
        {

            


            User? user = _DbContext.Users.FirstOrDefault(e => e.Email == loginForm.Email);

            
            if (ModelState.IsValid)
            {
                
                //user's role 

                string RoleName = Enum.GetName(typeof(Role), user.Role)!;

                //we create cookies  
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Role,RoleName),
                        new Claim("Email",user.Email)
                    };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = true,
                };

                //we sign in the user 

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
                );

                return RedirectToAction("Index", "admin");
            }

            else
            {
                
                return View("Index", loginForm);
            }




        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View("~/Views/Register/Index.cshtml");

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel registerForm)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with validation errors
                return View("~/Views/Register/Index.cshtml", registerForm);
            }

            //we check the email if it exists
            if (_DbContext.Users.Any(e => e.Email == registerForm.Email)) {
                ModelState.AddModelError("Email", "Email already exisit");
                // Return the view with validation errors
                return View("~/Views/Register/Index.cshtml", registerForm);
            }

            //create the User instance
            var newUser = new User { 
                Email = registerForm.Email,
                FirstName = registerForm.FirstName,
                LastName = registerForm.LastName,
                
            };
            //we hash the password and store it into the database 
            var hasher = new PasswordHasher<User>();
            string hashedPassword = hasher.HashPassword(newUser, registerForm.Password);

            newUser.Password = hashedPassword;

            _DbContext.Add(newUser);
            _DbContext.SaveChanges();   


            return RedirectToAction("index", "Authentication");
        }
    }
}
