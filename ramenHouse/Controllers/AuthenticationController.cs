using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ramenHouse.Data;
using ramenHouse.Models;
using ramenHouse.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

            if (user == null || user.Password != loginForm.Password)
            {
                
                ViewBag.ErrorMessage = "Invalid email or password";
                return View("Index", loginForm);
            }

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

                return RedirectToAction("Index", "Home");
            }

            else
            {
                //add an model level error 
                ModelState.AddModelError("", "Invalid email or password");
                return View("Index", loginForm);
            }




        }



    }
}
